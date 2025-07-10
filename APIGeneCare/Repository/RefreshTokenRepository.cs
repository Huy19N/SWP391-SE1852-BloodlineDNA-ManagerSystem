using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.AppSettings;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIGeneCare.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly GeneCareContext _context;
        private readonly Jwt _appSettings;

        public RefreshTokenRepository(GeneCareContext context,
            IOptionsMonitor<Jwt> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }
        public async Task<RefreshToken?> IsRefreshTokenValid(string token)
        {
            try
            {
                return await _context.RefreshTokens.Where(rt => rt.Token == token && rt.ExpiredAt > DateTime.UtcNow && !rt.Revoked)
                .FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<TokenModel> GenerateTokenModel(UserDTO user)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
                var role = _context.Roles.SingleOrDefault(r => r.RoleId == user.RoleId);
                if (role == null || String.IsNullOrWhiteSpace(role.RoleName)) return null!;
                var jwtId = DateTime.Now.Ticks.ToString() + Guid.NewGuid().ToString();

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim(ClaimTypes.Role, role.RoleName),
                        new Claim(JwtRegisteredClaimNames.Jti, jwtId)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha512Signature)
                };

                var accessToken = jwtTokenHandler.CreateToken(tokenDescription);
                var refreshToken = await GenerateRefreshToken(user, null, jwtId);
                return new TokenModel
                {
                    AccessToken = jwtTokenHandler.WriteToken(accessToken),
                    RefreshToken = refreshToken,
                    Role = 1,
                    userId = user.UserId
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> GenerateAccessTokenByRefToken(string token)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var refreshToken = await IsRefreshTokenValid(token);
                if (refreshToken == null)
                {
                    return null!;
                }

                var user = await _context.Users.FindAsync(refreshToken.UserId);
                if (user == null)
                {
                    return null!;
                }

                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
                var role = _context.Roles.SingleOrDefault(r => r.RoleId == user.RoleId);
                if (role == null || String.IsNullOrWhiteSpace(role.RoleName)) return null!;
                var jwtId = DateTime.Now.Ticks.ToString() + Guid.NewGuid().ToString();

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim(ClaimTypes.Role, role.RoleName),
                        new Claim(JwtRegisteredClaimNames.Jti, jwtId)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var accessToken = jwtTokenHandler.CreateToken(tokenDescription);

                refreshToken.JwtId = jwtId;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return jwtTokenHandler.WriteToken(accessToken);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<string> GenerateRefreshToken(UserDTO user, string? oldRefreshToken, string jwtId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                //clear expired refresh tokens
                var expiredTokens = await _context.RefreshTokens
                .Where(rt => rt.ExpiredAt < DateTime.UtcNow)
                .ToListAsync();

                if (expiredTokens.Any())
                {
                    foreach (var x in expiredTokens)
                    {
                        // Remove the refresh token from the logLogin table
                        var logLogin = await _context.LogLogins
                            .Where(ll => ll.RefreshTokenId == x.RefreshTokenId)
                            .ToArrayAsync();
                        if (logLogin != null)
                        {
                            _context.LogLogins.RemoveRange(logLogin);
                            await _context.SaveChangesAsync();
                        }
                    }
                    _context.RefreshTokens.RemoveRange(expiredTokens);
                    await _context.SaveChangesAsync();
                }

                var random = new byte[125];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(random);
                string token = Convert.ToBase64String(random);


                var newRefreshToken = new RefreshToken
                {
                    UserId = user.UserId,
                    Token = token,
                    JwtId = jwtId,
                    CreatedAt = DateTime.UtcNow,
                    ExpiredAt = DateTime.UtcNow.AddMinutes(_appSettings.MinRefreshExpirationTime),
                    Revoked = false,
                    Ipaddress = user.IPAddress,
                    UserAgent = user.UserAgent,
                    DeviceInfo = user.DeviceInfo
                };
                await _context.RefreshTokens.AddAsync(newRefreshToken);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return token;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public Task<bool> DeleteRefreshTokenByUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

    }
}
