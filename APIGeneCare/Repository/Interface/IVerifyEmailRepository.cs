// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Data;

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
