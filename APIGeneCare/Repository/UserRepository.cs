﻿using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.AppSettings;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.Extensions.Options;

namespace APIGeneCare.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GeneCareContext _context;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSettings _appSettings;
        public static int PAGE_SIZE { get; set; } = 10;

        public UserRepository(GeneCareContext context,
            IOptionsMonitor<JwtSettings> optionsMonitor,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
            _refreshTokenRepository = refreshTokenRepository;
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
