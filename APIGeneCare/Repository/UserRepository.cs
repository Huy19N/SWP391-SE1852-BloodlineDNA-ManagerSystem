using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
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
        public TokenModel GenerateToken(UserDTO user)
        {
            var jwtTokenHandler = new JsonWebTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var role = _context.Roles.SingleOrDefault(r => r.RoleId == user.RoleId);
            if (role == null || String.IsNullOrWhiteSpace(role.RoleName)) return null!;

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, role.RoleName),
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
                RefreshToken = refreshToken,
                Role = role.RoleId,
                userId = user.UserId,
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
        public UserDTO? Validate(LoginModel model)
        {
            var user = _context.Users
                .SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null) return null;

            return new UserDTO
            {
                UserId = user.UserId,
                RoleId = user.RoleId,
                FullName = user.FullName,
                Address = user.Address,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password
            };
        }

        public IEnumerable<UserDTO> GetAllUsersPaging(String? typeSearch, String? search, String? sortBy, int? page)
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

            return result.Select(u => new UserDTO
            {
                UserId = u.UserId,
                RoleId = u.RoleId,
                FullName = u.FullName,
                Address = u.Address,
                Email = u.Email,
                Phone = u.Phone
            });

        }
        public IEnumerable<UserDTO> GetAllUsers()
            => _context.Users.Select(u => new UserDTO
            {
                UserId = u.UserId,
                RoleId = u.RoleId,
                FullName = u.FullName,
                IdentifyId = u.IdentifyId,
                Address = u.Address,
                Email = u.Email,
                Phone = u.Phone,
                Password = u.Password
            }).OrderBy(u => u.UserId).ToList();
        public UserDTO? GetUserById(int id)
            => _context.Users.Select(u => new UserDTO
            {
                UserId = u.UserId,
                RoleId = u.RoleId,
                FullName = u.FullName,
                IdentifyId = u.IdentifyId,
                Address = u.Address,
                Email = u.Email,
                Phone = u.Phone,
                Password = u.Password
            }).SingleOrDefault(u => u.UserId == id);
        public UserDTO? GetUserByEmail(string email)
            => _context.Users.Select(u => new UserDTO
            {
                UserId = u.UserId,
                RoleId = u.RoleId,
                FullName = u.FullName,
                IdentifyId = u.IdentifyId,
                Address = u.Address,
                Email = u.Email,
                Phone = u.Phone,
                Password = u.Password
            }).SingleOrDefault(u => u.Email == email) ?? null!;
        public bool CreateUser(UserDTO user)
        {

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (user == null)
                {
                    return false;
                }

                _context.Users.Add(new User
                {
                    RoleId = user.RoleId,
                    Email = user.Email,
                    Password = user.Password
                });

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
        public bool UpdateUser(UserDTO user)
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
                existingUser.RoleId = user.RoleId;
                existingUser.FullName = user.FullName;
                existingUser.IdentifyId = user.IdentifyId;
                existingUser.Address = user.Address;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.Password = user.Password;


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
    }
}
