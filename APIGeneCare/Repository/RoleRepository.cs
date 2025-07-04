using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public RoleRepository(GeneCareContext context) => _context = context;
        public IEnumerable<RoleDTO> GetAllRoles()
            => _context.Roles.Select(r => new RoleDTO
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
            }).OrderBy(r => r.RoleId).ToList();

        public IEnumerable<RoleDTO> GetAllRolesPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allRoles = _context.Roles.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("roleid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int roleid))
                    {
                        allRoles = _context.Roles.Where(r => r.RoleId == roleid);
                    }
                }
                if (typeSearch.Equals("rolename", StringComparison.CurrentCultureIgnoreCase))
                    allRoles = _context.Roles.Where(r => !String.IsNullOrWhiteSpace(r.RoleName) &&
                    r.RoleName.Equals(r.RoleName, StringComparison.CurrentCultureIgnoreCase));

            }
            #endregion
            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("roleid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allRoles = allRoles.OrderBy(r => r.RoleId);

                if (sortBy.Equals("roleid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allRoles = allRoles.OrderByDescending(r => r.RoleId);

                if (sortBy.Equals("rolename_asc", StringComparison.CurrentCultureIgnoreCase))
                    allRoles = allRoles.OrderBy(r => r.RoleName);

                if (sortBy.Equals("rolename_desc", StringComparison.CurrentCultureIgnoreCase))
                    allRoles = allRoles.OrderByDescending(r => r.RoleName);

            }
            #endregion

            var result = PaginatedList<Role>.Create(allRoles, page ?? 1, PAGE_SIZE);
            return result.Select(r => new RoleDTO
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
            });
        }
        public RoleDTO? GetRoleById(int id)
            => _context.Roles.Select(r => new RoleDTO
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
            }).SingleOrDefault(r => r.RoleId == id);
        public bool CreateRole(RoleDTO role)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (role == null)
                {
                    return false;
                }

                _context.Roles.Add(new Role
                {
                    RoleName = role.RoleName,
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
        public bool UpdateRole(RoleDTO role)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (role == null)
                {
                    return false;
                }
                var existingRole = _context.Roles.Find(role.RoleId);
                if (existingRole == null)
                {
                    return false;
                }

                existingRole.RoleName = role.RoleName;

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
        public bool DeleteRoleById(int id)
        {
            
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var role = _context.Roles.Find(id);
                if (role == null) return false;

                _context.Roles.Remove(role);

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
