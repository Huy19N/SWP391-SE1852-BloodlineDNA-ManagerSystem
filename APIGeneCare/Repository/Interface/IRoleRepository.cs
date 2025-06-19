// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRolesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        Role? GetRoleById(int id);
        bool CreateRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRoleById(int id);
    }
}
