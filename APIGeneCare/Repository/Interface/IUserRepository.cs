using APIGeneCare.Data;
using APIGeneCare.Model;

namespace APIGeneCare.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers(string? typeSearch, string? search, string? sortBy, int? page);
        User? GetUserById(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        User? Validate(LoginModel model);
        public string GenerateToken(User user;
    }
}
