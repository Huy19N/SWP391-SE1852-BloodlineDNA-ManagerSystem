using APIGeneCare.Entities;
using APIGeneCare.Model.AppSettings;
using APIGeneCare.Repository.Interface;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;

namespace APIGeneCare.Repository
{
    public class VerifyEmailRepository : IVerifyEmailRepository
    {
        private readonly GeneCareContext _context;
        public readonly EmailSettings _emailSettings;
        public readonly AppSettings _appSettings;
        public readonly JwtSettings _jwtSettings;
        public readonly FontEndSettings _fontEndSettings;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public VerifyEmailRepository(GeneCareContext context,
            IOptionsMonitor<EmailSettings> emailSettings,
            IOptionsMonitor<AppSettings> appSettings,
            IOptionsMonitor<JwtSettings> jwtSettings,
            IOptionsMonitor<FontEndSettings> fontEnd)
        {
            _context = context;
            _emailSettings = emailSettings.CurrentValue;
            _appSettings = appSettings.CurrentValue;
            _jwtSettings = jwtSettings.CurrentValue;
            _fontEndSettings = fontEnd.CurrentValue;
        }

        public bool CreateVerifyEmail(VerifyEmail verifyEmail)
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
                throw;
            }
        }

        public bool DeleteVerifyEmailByEmail(String email)
        {
            if (email == null) return false;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.VerifyEmails.Remove(_context.VerifyEmails.FirstOrDefault(x => x.Email == email) ?? null!);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public VerifyEmail? GetVerifyEmailByEmail(string email, string otp)
            => _context.VerifyEmails.FirstOrDefault(x => x.Email == email && x.Otp == otp);

        public async Task<bool> SendConfirmEmail(string email)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
                
                Random random = new Random();
                string otp = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                if (string.IsNullOrWhiteSpace(email)) return false;
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
                    $"<br>\r\nVui lòng nhập mã OTP bên dưới để xác nhận email của bạn:\r\n    " +
                    $"</p>\r\n    " +
                    $"<a class=\"btn-confirm\">{otp}</a>\r\n    " +
                    $"<div class=\"footer\">\r\n      Nếu bạn không yêu cầu, vui lòng bỏ qua email này." +
                    $"<br>\r\n      Trân trọng," +
                    $"<br>\r\n      Đội ngũ GeneCare\r\n    " +
                    $"</div>\r\n  </div>\r\n" +
                    $"</body>\r\n" +
                    $"</html>\r\n";
                #endregion
                var verifyEmail = await _context.VerifyEmails.Where(x => x.Email.Trim().ToLower() == email.Trim().ToLower()).ToArrayAsync();

                await Task.Run(() => _context.VerifyEmails.RemoveRange(verifyEmail));
                await _context.SaveChangesAsync();

                _context.VerifyEmails.Add(new VerifyEmail
                {
                    Otp = otp,
                    Email = email,
                    IsResetPwd = false,
                    CreatedAt = timeNow,
                    ExpiredAt = timeNow.AddMinutes(_emailSettings.ExpiredConfirmAt)
                });

                await SendEmailAsync(email, "Confirm email by GeneCare", body);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<bool> SendEmailConfirmForgetPassword(string email)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);

                if (_context.Users.FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == email.Trim().ToLower()) == null) return false;
                
                Random random = new Random();
                string otp = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                if (string.IsNullOrWhiteSpace(email)) return false;
                #region body
                var body = $@"
                <!DOCTYPE html>
                <html lang=""vi"">
                <head>
                  <meta charset=""UTF-8"">
                  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                  <title>Quên mật khẩu - GeneCare</title>
                  <link href=""https://fonts.googleapis.com/css2?family=Quicksand:wght@400;600;700&display=swap"" rel=""stylesheet"">
                  <style>
                    @keyframes fadeIn {{
                      from {{ opacity: 0; transform: scale(0.9); }}
                      to {{ opacity: 1; transform: scale(1); }}
                    }}
                    @keyframes bounce {{
                      0%, 20%, 50%, 80%, 100% {{ transform: translateY(0); }}
                      40% {{ transform: translateY(-15px); }}
                      60% {{ transform: translateY(-7px); }}
                    }}
                    @keyframes gradientShift {{
                      0% {{ background-position: 0% 50%; }}
                      50% {{ background-position: 100% 50%; }}
                      100% {{ background-position: 0% 50%; }}
                    }}
                    @keyframes sparkle {{
                      0% {{ opacity: 0; transform: translateY(0); }}
                      50% {{ opacity: 1; transform: translateY(10px); }}
                      100% {{ opacity: 0; transform: translateY(20px); }}
                    }}
                    body {{
                      margin: 0;
                      padding: 0;
                      font-family: 'Quicksand', sans-serif;
                      background: linear-gradient(135deg, #FFF0F5, #E0FFFF);
                      overflow: hidden;
                    }}
                    .star {{
                      position: absolute;
                      width: 8px;
                      height: 8px;
                      background: rgba(255,255,255,0.8);
                      border-radius: 50%;
                      animation: sparkle 3s infinite;
                    }}
                    .container {{
                      background: #fff;
                      max-width: 650px;
                      margin: 60px auto;
                      border-radius: 20px;
                      box-shadow: 0 12px 50px rgba(0,0,0,0.25);
                      padding: 50px 40px;
                      text-align: center;
                      position: relative;
                      animation: fadeIn 1.2s ease-out;
                      border: 3px solid #FFC0CB;
                    }}
                    .container::before {{
                      content: '';
                      position: absolute;
                      top: -5px; left: -5px; right: -5px; bottom: -5px;
                      border-radius: 25px;
                      background: linear-gradient(270deg, #FFDEE9, #B5FFFC, #FFDEE9);
                      background-size: 600% 600%;
                      z-index: -1;
                      animation: gradientShift 10s ease infinite;
                      filter: blur(15px);
                    }}
                    .icon {{
                      font-size: 80px;
                      color: #FF69B4;
                      margin-bottom: 25px;
                      animation: bounce 2s infinite;
                      text-shadow: 0 4px 20px rgba(255,105,180,0.6);
                    }}
                    h2 {{
                      color: #FF1493;
                      font-size: 30px;
                      margin-bottom: 18px;
                      font-weight: 700;
                      letter-spacing: 1px;
                    }}
                    p {{
                      color: #555;
                      font-size: 17px;
                      line-height: 1.8;
                      margin-bottom: 35px;
                      max-width: 550px;
                      margin-left: auto;
                      margin-right: auto;
                    }}
                    p strong {{
                      color: #FF1493;
                    }}
                    .btn-confirm {{
                      display: inline-block;
                      padding: 18px 48px;
                      background: linear-gradient(90deg, #FF6FD8 0%, #3813C2 100%);
                      color: #fff;
                      border-radius: 12px;
                      text-decoration: none;
                      font-weight: 700;
                      font-size: 19px;
                      transition: all 0.4s ease;
                      box-shadow: 0 8px 25px rgba(255,105,180,0.4);
                      text-transform: uppercase;
                      position: relative;
                      overflow: hidden;
                    }}
                    .btn-confirm::before {{
                      content: '';
                      position: absolute;
                      top: 0;
                      left: -100%;
                      width: 100%;
                      height: 100%;
                      background: linear-gradient(120deg, transparent, rgba(255,255,255,0.4), transparent);
                      transition: all 0.7s ease;
                      transform: skewX(-20deg);
                    }}
                    .btn-confirm:hover {{
                      transform: translateY(-5px) scale(1.05);
                      box-shadow: 0 15px 40px rgba(255,105,180,0.6);
                    }}
                    .btn-confirm:hover::before {{
                      left: 100%;
                    }}
                    .footer {{
                      margin-top: 50px;
                      color: #888;
                      font-size: 14px;
                      line-height: 1.7;
                      border-top: 1px dashed #ddd;
                      padding-top: 25px;
                    }}
                    .footer strong {{
                      color: #FF1493;
                    }}
                    @media (max-width: 700px) {{
                      .container {{
                        padding: 40px 25px;
                      }}
                      .icon {{ font-size: 60px; }}
                      h2 {{ font-size: 26px; }}
                      p {{ font-size: 16px; }}
                      .btn-confirm {{
                        padding: 14px 30px;
                        font-size: 17px;
                      }}
                    }}
                  </style>
                </head>
                <body>
                  <div class=""star"" style=""top:10%; left:20%; animation-delay:0s;""></div>
                  <div class=""star"" style=""top:30%; left:70%; animation-delay:1s;""></div>
                  <div class=""star"" style=""top:50%; left:40%; animation-delay:2s;""></div>
                  <div class=""star"" style=""top:70%; left:80%; animation-delay:1.5s;""></div>
                  <div class=""container"">
                    <div class=""icon"">&#128302;</div>
                    <h2>Thắp Lại Ánh Sáng Cho Hành Trình Của Bạn!</h2>
                    <p>
                      Xin chào, người dùng của <strong>GeneCare</strong>!<br><br>
                      Đôi khi trong quá trình hoạt động. Chúng tôi vừa nhận được yêu cầu <strong>khôi phục mật khẩu</strong> cho tài khoản của bạn.<br>
                      Đừng lo lắng đây là mã OTP của bạn.<br>
                    </p>
                    <a class=""btn-confirm"">{otp}</a>
                    <div class=""footer"">
                      Nếu bạn không phải người gửi yêu cầu, xin hãy bỏ qua email này.<br><br>
                      Luôn đồng hành và bảo vệ bạn,<br>
                      <strong>Đội ngũ GeneCare</strong>
                    </div>
                  </div>
                </body>
                </html>
                ";
                #endregion

                var verifyEmail = await _context.VerifyEmails.Where(x => x.Email.Trim().ToLower() == email.Trim().ToLower()).ToArrayAsync();

                await Task.Run(() => _context.VerifyEmails.RemoveRange(verifyEmail));
                await _context.SaveChangesAsync();

                bool isSave = await Task.Run(() => CreateVerifyEmail(new VerifyEmail
                {
                    Otp = otp,
                    Email = email,
                    IsResetPwd = true,
                    CreatedAt = timeNow,
                    ExpiredAt = timeNow.AddMinutes(_emailSettings.ExpiredConfirmAt)
                }));
                await SendEmailAsync(email, "Confirm email by GeneCare", body);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ConfirmForgetPassword(string email, string otp, string password)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);

                var verifyEmail = await _context.VerifyEmails.Where(x => x.Otp == otp &&
                    x.Email.Trim().ToLower() == email.Trim().ToLower() && x.IsResetPwd).ToArrayAsync();

                if (verifyEmail == null || verifyEmail.Length == 0)
                    return false;

                _context.VerifyEmails.RemoveRange(verifyEmail);
                await _context.SaveChangesAsync();

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == email.Trim().ToLower());

                if (user != null)
                {
                    user.Password = password;
                    user.LastPwdChange = timeNow;

                    var tokens = await _context.RefreshTokens
                        .Where(x => x.UserId == user.UserId)
                        .ToListAsync();

                    foreach (var token in tokens)
                    {
                        token.Revoked = true;
                        _context.AccessTokenBlacklists.Add(new AccessTokenBlacklist
                        {
                            JwtId = token.JwtId,
                            UserId = token.UserId,
                            ExpireAt = timeNow.AddMinutes(_jwtSettings.MinAccessExpirationTime),
                            Reason = "Password changed"
                        });
                    }

                    await _context.SaveChangesAsync();
                }

                else
                {
                    await transaction.RollbackAsync();
                    throw new Exception("User not found");
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<bool> ConfirmEmail(string email, string otp)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);

                var verifyEmail = await _context.VerifyEmails.Where(x => x.Otp == otp &&
                    x.Email.Trim().ToLower() == email.Trim().ToLower() && !x.IsResetPwd).ToArrayAsync();

                if (verifyEmail == null || verifyEmail.Length == 0)
                    return false;

                await Task.Run(() => _context.VerifyEmails.RemoveRange(verifyEmail));
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> CheckFo(string email)
        {
            var verifyEmail = await _context.VerifyEmails.FirstOrDefaultAsync(x => x.Email.Trim().ToLower() == email.Trim().ToLower());
            return verifyEmail == null;
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("GeneCare", _emailSettings.SmtpUser));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;
                message.Body = new TextPart("html") { Text = body };

                using var client = new SmtpClient();
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error SendEmailAsync: " + ex);
            }

        }
    }
}
