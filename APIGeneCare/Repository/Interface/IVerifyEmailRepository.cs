using APIGeneCare.Entities;

namespace APIGeneCare.Repository.Interface
{
    public interface IVerifyEmailRepository
    {
        Task<bool> SendConfirmEmail(string email);
        Task<bool> ConfirmEmail(string email, string key);

        Task<bool> SendEmailConfirmForgetPassword(string email);
        Task<bool> ConfirmForgetPassword(string email, string key);

        VerifyEmail? GetVerifyEmailByEmail(string email);
        bool CreateVerifyEmail(VerifyEmail verifyEmail);
        bool UpdateVerifyEmail(VerifyEmail verifyEmail);
        bool DeleteVerifyEmailByEmail(string email);
    }
}
