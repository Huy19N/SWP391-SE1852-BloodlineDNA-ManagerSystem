using APIGeneCare.Model;

namespace APIGeneCare.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<Object?> Login(LoginModel model, HttpContext context);
        string GenerateUrlGoogleLogin();
        Task<string?> GoogleLoginCallback(string code, string state, HttpContext context);

    }
}
