using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IRoleRepository
    {
        IEnumerable<RoleDTO> GetAllRolesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<RoleDTO> GetAllRoles();
        RoleDTO? GetRoleById(int id);
        bool CreateRole(RoleDTO role);
        bool UpdateRole(RoleDTO role);
        bool DeleteRoleById(int id);
    }
}
