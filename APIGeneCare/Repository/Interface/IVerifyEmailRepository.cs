// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IVerifyEmailRepository
    {
        bool ConfirmEmail(string email, string key);
        Task<bool> SendConfirmEmail(string email, string apiConfirmEmail);
        VerifyEmailDTO? GetVerifyEmailByEmail(string email);
        bool CreateVerifyEmail(VerifyEmailDTO verifyEmail);
        bool UpdateVerifyEmail(VerifyEmailDTO verifyEmail);
        bool DeleteVerifyEmailByEmail(string email);
    }
}
