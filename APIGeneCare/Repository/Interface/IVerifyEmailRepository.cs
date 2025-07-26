using APIGeneCare.Entities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace APIGeneCare.Repository.Interface
{
    public interface IVerifyEmailRepository
    {
        Task<bool> SendConfirmEmail(string email);
        Task<bool> ConfirmEmail(string email, string otp);

        Task<bool> SendEmailConfirmForgetPassword(string email);
        Task<bool> ConfirmForgetPassword(string email, string otp, string password);

        VerifyEmail? GetVerifyEmailByEmail(string email, string otp);
        bool CreateVerifyEmail(VerifyEmail verifyEmail);
        bool DeleteVerifyEmailByEmail(string email);
    }
}
