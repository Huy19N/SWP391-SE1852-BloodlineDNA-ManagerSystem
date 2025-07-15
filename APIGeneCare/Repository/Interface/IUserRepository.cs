using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<UserDTO> GetAllUsersPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO? GetUserById(int id);
        UserDTO? GetUserByEmail(string email);
        bool CreateUser(UserDTO user);
        bool UpdateUser(UserDTO user);
        bool DeleteUserById(int id);
    }
}
