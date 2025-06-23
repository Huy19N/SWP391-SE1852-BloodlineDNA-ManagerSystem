// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class VerifyEmailRepository : IVerifyEmailRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public VerifyEmailRepository(GeneCareContext context) => _context = context;
        public bool CreateVerifyEmail(VerifyEmailDTO verifyEmail)
        {
            if (verifyEmail == null) return false;
            if (_context.VerifyEmails.Find(verifyEmail.Email) != null) return false;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.VerifyEmails.Add(verifyEmail);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool DeleteVerifyEmailByEmail(String email)
        {
            if (email == null) return false;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.VerifyEmails.Remove(GetVerifyEmailByEmail(email) ?? null!);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public VerifyEmailDTO? GetVerifyEmailByEmail(string email)
            => _context.VerifyEmails.Find(email);
        public bool UpdateVerifyEmail(VerifyEmailDTO verifyEmail)
        {
            if (verifyEmail == null) return false;
            var existVerifyEmail = GetVerifyEmailByEmail(verifyEmail.Email);
            if (existVerifyEmail == null) return false;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existVerifyEmail.ExpiredAt = verifyEmail.ExpiredAt;
                existVerifyEmail.CreatedAt = verifyEmail.CreatedAt;
                existVerifyEmail.Key = verifyEmail.Key;
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public async Task<bool> SendConfirmEmail(string email, string apiConfirmEmail)
        {
            try
            {
                var send = new SendConfirmEmailModel();
                var key = Guid.NewGuid().ToString();
                if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(apiConfirmEmail)) return false;
                #region body
                var body = $"<!DOCTYPE html>\r\n" +
                    $"<html>" +
                    $"\r\n<head>\r\n  " +
                    $"<meta charset=\"UTF-8\">\r\n  " +
                    $"<title>Xác nhận Email</title>\r\n  " +
                    $"<style>\r\n    " +
                    $"body " +
                    $"{{\r\n      " +
                    $"font-family: Arial, sans-serif;\r\n      " +
                    $"background: #f6f6f6;\r\n      " +
                    $"margin: 0;\r\n      " +
                    $"padding: 0;\r\n    " +
                    $"}}\r\n    " +
                    $".container {{\r\n      " +
                    $"background: #fff;\r\n      " +
                    $"max-width: 480px;\r\n      " +
                    $"margin: 40px auto;\r\n      " +
                    $"border-radius: 8px;\r\n      " +
                    $"box-shadow: 0 2px 8px rgba(0,0,0,0.07);\r\n      " +
                    $"padding: 32px 24px;\r\n      " +
                    $"text-align: center;\r\n    }}\r\n    " +
                    $"h2 " +
                    $"{{\r\n      " +
                    $"color: #2d7ff9;\r\n      " +
                    $"margin-bottom: 16px;\r\n    " +
                    $"}}\r\n    " +
                    $"p " +
                    $"{{\r\n      " +
                    $"color: #333;\r\n      " +
                    $"font-size: 16px;\r\n      " +
                    $"margin-bottom: 32px;\r\n   " +
                    $" }}\r\n    " +
                    $".btn-confirm " +
                    $"{{\r\n      " +
                    $"display: inline-block;\r\n      " +
                    $"padding: 12px 32px;\r\n      " +
                    $"background: #2d7ff9;\r\n      " +
                    $"color: #fff !important;\r\n      " +
                    $"border-radius: 4px;\r\n      " +
                    $"text-decoration: none;\r\n      " +
                    $"font-weight: bold;\r\n      " +
                    $"font-size: 16px;\r\n      " +
                    $"transition: background 0.2s;\r\n    " +
                    $"}}\r\n    " +
                    $".btn-confirm:hover " +
                    $"{{\r\n      " +
                    $"background: #1a5fc2;\r\n    " +
                    $"}}\r\n    .footer {{\r\n      " +
                    $"margin-top: 32px;\r\n      " +
                    $"color: #888;\r\n      " +
                    $"font-size: 13px;\r\n    " +
                    $"}}\r\n  " +
                    $"</style>\r\n" +
                    $"</head>\r\n" +
                    $"<body>\r\n  " +
                    $"<div class=\"container\">\r\n    " +
                    $"<h2>Xác nhận Email của bạn</h2>\r\n    " +
                    $"<p>Chào bạn," +
                    $"<br>\r\nCảm ơn bạn đã đăng ký tài khoản tại GeneCare." +
                    $"<br>\r\nVui lòng nhấn nút bên dưới để xác nhận email của bạn:\r\n    " +
                    $"</p>\r\n    " +
                    $"<a href=\"{apiConfirmEmail}email={email}&key={key}\" class=\"btn-confirm\">Xác nhận Email</a>\r\n    " +
                    $"<div class=\"footer\">\r\n      Nếu bạn không đăng ký tài khoản, vui lòng bỏ qua email này." +
                    $"<br>\r\n      Trân trọng," +
                    $"<br>\r\n      Đội ngũ GeneCare\r\n    " +
                    $"</div>\r\n  </div>\r\n" +
                    $"</body>\r\n" +
                    $"</html>\r\n";
                #endregion
                var verifyEmail = GetVerifyEmailByEmail(email);
                if (verifyEmail != null)
                {
                    bool isSave = await Task.Run(() => UpdateVerifyEmail(new VerifyEmailDTO
                    {
                        Email = email,
                        CreatedAt = DateTime.Now,
                        ExpiredAt = DateTime.Now.AddMinutes(10),
                        Key = key,
                    }));
                }
                else
                {
                    bool isSave = await Task.Run(() => CreateVerifyEmail(new VerifyEmailDTO
                    {
                        Email = email,
                        CreatedAt = DateTime.Now,
                        ExpiredAt = DateTime.Now.AddMinutes(10),
                        Key = key,
                    }));
                }

                await send.SendEmailAsync(email, "Confirm email by GeneCare", body);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ConfirmEmail(string email, string key)
        {
            try
            {
                var verifyEmail = GetVerifyEmailByEmail(email);

                if (verifyEmail?.ExpiredAt < DateTime.Now)
                    return false;
                return DeleteVerifyEmailByEmail(email);
            }
            catch
            {
                return false;
            }
        }
    }
}
