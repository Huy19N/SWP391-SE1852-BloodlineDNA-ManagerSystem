using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace APIGeneCare.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly GeneCareContext _context;
        private IConfiguration _configuration;
        public RefreshTokenRepository(GeneCareContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> IsRefreshTokenValid(string token)
        {
            try
            {
                return await _context.RefreshTokens.Where(rt => rt.Token == token && rt.ExpiredAt > DateTime.UtcNow)
                .FirstOrDefaultAsync()
                .ContinueWith(task => task.Result != null);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string?> GenerateRefreshToken(UserDTO user, string accessToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var User = _context.Users.FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == user.Email.Trim().ToLower() && u.Password == user.Password);
                if (User == null)
                {
                    return null; // User not found or invalid credentials
                }
                
                var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == User.Id );
                
                var random = new byte[125];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(random);
                string newRefreshtoken = Convert.ToBase64String(random);
                _context.RefreshTokens.Add(new RefreshToken
                {
                    UserId = User.Id,
                    Token = refreshToken,
                    JwtId = accessToken,
                    IssueAt = DateTime.UtcNow,
                    ExpiredAt = DateTime.UtcNow.AddMinutes(double.TryParse(_configuration["AppSettings:MinRefreshExpirationTime"], out var a)? a : 0) // Set expiration time to 30 minutes
                });

                _context.SaveChanges();
                await transaction.CommitAsync();
                return refreshToken;
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
