using APIGeneCare.Data;
using APIGeneCare.Model;

namespace APIGeneCare.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsersPaging(string? typeSearch, string? search, string? sortBy, int? page);
        User? GetUserById(int id);
        User? GetUserByEmail(string email);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUserById(int id);
        User? Validate(LoginModel model);
        TokenModel GenerateToken(User user);
    }
}
