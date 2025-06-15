using APIGeneCare.Data;
using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MimeKit;
using System.Drawing;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIGeneCare.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        private readonly AppSettings _appSettings;

        public UserRepository(GeneCareContext context, 
            IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }
        public TokenModel GenerateToken(User user)
        {
            var jwtTokenHandler = new JsonWebTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var role = _context.Roles.SingleOrDefault(r => r.RoleId == user.RoleId);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email ?? null!),
                    new Claim(ClaimTypes.Name, user.FullName ?? null!),
                    new Claim("id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, role?.RoleName ?? null!),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var accessToken = jwtTokenHandler.CreateToken(tokenDescription);
            var refreshToken = GenerateRefreshToken();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        private string GenerateRefreshToken()
        {
            var random = new byte[125];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        public User? Validate(LoginModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email &&
            u.Password == model.Password);
            return user;
        }
        public Boolean CreateUser(User user)
        {
            if (user == null)
            {
                return false;
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool DeleteUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Users.Remove(user);

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public IEnumerable<User> GetAllUsersPaging(String? typeSearch, String? search, String? sortBy, int? page)
        {
            var allUsers = _context.Users.AsQueryable();

            #region Search by type
            if (!string.IsNullOrWhiteSpace(typeSearch) && !string.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Trim().Equals("id", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int userId))
                    {
                        allUsers = _context.Users.Where(u => u.UserId == userId);
                    }
                }

                if (typeSearch.Trim().Equals("name", StringComparison.CurrentCultureIgnoreCase))
                {
                    allUsers = _context.Users.Where(u => u.FullName != null && u.FullName.Contains(search, StringComparison.InvariantCultureIgnoreCase));
                }

                if (typeSearch.Trim().Equals("email", StringComparison.CurrentCultureIgnoreCase))
                {
                    allUsers = _context.Users.Where(u => u.Email != null && u.Email.Trim().Contains(search.Trim(), StringComparison.CurrentCultureIgnoreCase));
                }

            }
            #endregion

            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Trim().Equals("id_asc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allUsers = allUsers.OrderBy(u => u.UserId);
                }

                if (sortBy.Trim().Equals("id_desc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allUsers = allUsers.OrderByDescending(u => u.UserId);
                }

                if (sortBy.Trim().Equals("name_asc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allUsers = allUsers.OrderBy(u => u.FullName);
                }

                if (sortBy.Trim().Equals("name_desc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allUsers = allUsers.OrderByDescending(u => u.FullName);
                }

                if (sortBy.Trim().Equals("email_asc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allUsers = allUsers.OrderBy(u => u.Email);
                }

                if (sortBy.Trim().Equals("email_desc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allUsers = allUsers.OrderByDescending(u => u.Email);
                }

            }

            #endregion

            var result = PaginatedList<User>.Create(allUsers, page ?? 1, PAGE_SIZE);

            return result.Select(u => new User
            {
                UserId = u.UserId,
                RoleId = u.RoleId,
                FullName = u.FullName,
                Address = u.Address,
                Email = u.Email,
                Phone = u.Phone
            });

        }
        public User? GetUserById(int id)
            => _context.Users.FirstOrDefault(u => u.UserId == id);
        public User? GetUserByEmail(string email)
            => _context.Users.SingleOrDefault(u => u.Email == email) ?? null!;
        public bool UpdateUser(User user)
        {
            if (user == null)
            {
                return false;
            }
            var existingUser = _context.Users.Find(user.UserId);
            if (existingUser == null)
            {
                return false;
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existingUser.FullName = user.FullName;
                existingUser.Address = user.Address;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.Password = user.Password;
                existingUser.RoleId = user.RoleId;

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
