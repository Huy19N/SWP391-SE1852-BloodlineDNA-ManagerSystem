using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRolesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        Role? GetRoleById(int id);
        bool CreateRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(int id);
    }
}
