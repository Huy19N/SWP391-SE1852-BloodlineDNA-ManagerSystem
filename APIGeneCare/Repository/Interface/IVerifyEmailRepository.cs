using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IVerifyEmailRepository
    {
        bool ConfirmEmail(string email, string key);
        Task<bool> SendConfirmEmail(string email, string apiConfirmEmail);
        VerifyEmail? GetVerifyEmailByEmail(string email);
        bool CreateVerifyEmail(VerifyEmail verifyEmail);
        bool UpdateVerifyEmail(VerifyEmail verifyEmail);
        bool DeleteVerifyEmailByEmail(string email);
    }
}
