using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APIGeneCare.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GeneCareContext _context;
        private readonly Jwt _appSettings;
        public static int PAGE_SIZE { get; set; } = 10;

        public UserRepository(GeneCareContext context,
            IOptionsMonitor<Jwt> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }
        public async Task<Object?> Login(LoginModel model, HttpContext context)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null) return null;
            var IpAddress = context.Connection.RemoteIpAddress?.ToString();
            var UserAgent = context.Request.Headers["User-Agent"].ToString();

            if (!string.IsNullOrEmpty(model.Password) &&
                user.Password != model.Password)
            {
                var log = await _context.LogLogins.Where(x => x.Ipaddress == IpAddress)
                    .OrderByDescending(x => x.LoginTime)
                    .Take(_appSettings.MaxCountOfLogin)
                    .ToListAsync();

                int count = 0;
                foreach (var x in log)
                {
                    if (x.Success) break;
                    count++;
                }
                if (count > 0)
                {
                    var nearlyFailedLogin = log.First().LoginTime;
                    if (nearlyFailedLogin.AddMinutes(_appSettings.LockoutTimeEachFaildCountInMinutes * count) < DateTime.UtcNow)
                    {
                        return new LockResponseModel
                        {
                            Success = false,
                            Message = "Your account is locked due to too many failed login attempts. Please try again later.",
                            LockoutEnd = nearlyFailedLogin.AddMinutes(_appSettings.LockoutTimeEachFaildCountInMinutes * count)
                        };
                    }
                }
                count++;
                await _context.LogLogins.AddAsync(new LogLogin
                {
                    UserId = user.UserId,
                    RefreshTokenId = null,
                    Success = false,
                    FailReason = "Invalid password",
                    Ipaddress = IpAddress,
                    UserAgent = UserAgent,
                    LoginTime = DateTime.UtcNow,
                });

                _context.SaveChanges();
                return new LockResponseModel
                {
                    Success = false,
                    Message = "Your account is locked due to too many failed login attempts. Please try again later.",
                    LockoutEnd = DateTime.UtcNow.AddMinutes(_appSettings.LockoutTimeEachFaildCountInMinutes * count)
                };
            }
            ;

            var UserDTO = new UserDTO
            {
                UserId = user.UserId,
                RoleId = user.RoleId,
                IdentifyId = user.IdentifyId,
                FullName = user.FullName,
                Address = user.Address,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                IPAddress = context.Connection.RemoteIpAddress?.ToString(),
                LastPwdChange = user.LastPwdChange,
            };

            var token = await new RefreshTokenRepository(_context, (IOptionsMonitor<Jwt>)_appSettings).GenerateTokenModel(UserDTO);
            return token;
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
                throw;
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
                throw;
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
                throw;
            }
        }
    }
}
