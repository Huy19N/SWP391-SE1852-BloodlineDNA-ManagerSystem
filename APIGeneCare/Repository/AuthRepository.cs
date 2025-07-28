using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.AppSettings;
using APIGeneCare.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace APIGeneCare.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly GeneCareContext _context;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly AppSettings _appSettings;
        private readonly GoogleLoginSettings _googleLoginSettings;

        public AuthRepository(GeneCareContext context,
            IRefreshTokenRepository refreshTokenRepository,
            IOptionsMonitor<JwtSettings> jwtSettings,
            IOptionsMonitor<AppSettings> appSettings,
            IOptionsMonitor<GoogleLoginSettings> googleLoginSettings)
        {
            _context = context;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings.CurrentValue;
            _appSettings = appSettings.CurrentValue;
            _googleLoginSettings = googleLoginSettings.CurrentValue;
        }

        public async Task<Object?> Login(LoginModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
            var dateNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null) return null;
            var ipAddress = string.Empty;
            try
            {
                var remoteIpAddress = context.Connection.RemoteIpAddress;

                if (remoteIpAddress != null)
                {
                    if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                            .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                    }

                    if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

                }
            }
            catch
            {
                ipAddress = "127.0.0.1";
            }
            var UserAgent = context.Request.Headers["User-Agent"].ToString();

            if (!string.IsNullOrEmpty(model.Password) &&
                user.Password != model.Password)
            {
                var log = await _context.LogLogins.Where(x => x.UserId == user.UserId)
                    .OrderByDescending(x => x.LoginTime)
                    .Take(_jwtSettings.MaxCountOfLogin)
                    .ToListAsync();

                int count = 0;
                foreach (var x in log)
                {
                    if (x.Success) break;
                    count++;
                }
                if (count > 0)
                {
                    var nearlyFailedLogin = log.First().LoginTime;
                    if (nearlyFailedLogin.AddMinutes(_jwtSettings.LockoutTimeEachFaildCountInMinutes * count) < DateTime.UtcNow)
                    {
                        return new LockResponseModel
                        {
                            Success = false,
                            Message = "Your account is locked due to too many failed login attempts. Please try again later.",
                            LockoutEnd = nearlyFailedLogin.AddMinutes(_jwtSettings.LockoutTimeEachFaildCountInMinutes * count)
                        };
                    }
                }
                count++;
                await _context.LogLogins.AddAsync(new LogLogin
                {
                    UserId = user.UserId,
                    RefreshTokenId = null,
                    Success = false,
                    FailReason = "Invalid password",
                    Ipaddress = ipAddress,
                    UserAgent = UserAgent,
                    LoginTime = dateNow,
                });

                _context.SaveChanges();
                return new LockResponseModel
                {
                    Success = false,
                    Message = "Your account is locked due to too many failed login attempts. Please try again later.",
                    LockoutEnd = dateNow.AddMinutes(_jwtSettings.LockoutTimeEachFaildCountInMinutes * count)
                };
            }
            ;

            var userRefeshToken = new UserRefeshToken
            {
                UserId = user.UserId,
                RoleId = user.RoleId,
                IdentifyId = user.IdentifyId,
                FullName = user.FullName,
                Address = user.Address,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                IPAddress = ipAddress,
                LastPwdChange = user.LastPwdChange,
            };
            var token = await _refreshTokenRepository.GenerateTokenModel(userRefeshToken);
            return token;
        }


        public string GenerateUrlGoogleLogin()
        {
            try
            {
                var clientId = _googleLoginSettings.ClientId;
                var redirectUri = _googleLoginSettings.RedirectUrl;
                var scope = _googleLoginSettings.Scope;
                var state = Guid.NewGuid().ToString();

                var url = $"https://accounts.google.com/o/oauth2/v2/auth?response_type=code" +
                          $"&client_id={clientId}" +
                          $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
                          $"&scope={Uri.EscapeDataString(scope)}" +
                          $"&state={state}" +
                          $"&access_type=offline" +
                          $"&prompt=consent";

                return url;
            }
            catch
            {
                throw;
            }

        }

        public async Task<string?> GoogleLoginCallback(string code, string state, HttpContext context)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
                var dateNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);

                var ipAddress = string.Empty;
                try
                {
                    var remoteIpAddress = context.Connection.RemoteIpAddress;

                    if (remoteIpAddress != null)
                    {
                        if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                                .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                        }

                        if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

                    }
                }
                catch
                {
                    ipAddress = "127.0.0.1";
                }
                var UserAgent = context.Request.Headers["User-Agent"].ToString();

                // 1. Lấy access_token từ Google
                var clientId = _googleLoginSettings.ClientId;
                var clientSecret = _googleLoginSettings.ClientSecret;
                var redirectUrl = _googleLoginSettings.RedirectUrl;

                var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token")
                {
                    

                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"code", code},
                        {"client_id", clientId},
                        {"client_secret", clientSecret},
                        {"redirect_uri", redirectUrl},
                        {"grant_type", "authorization_code"}
                    })
                };

                using var httpClient = new HttpClient();
                var tokenResponse = await httpClient.SendAsync(tokenRequest);
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                if (!tokenResponse.IsSuccessStatusCode) return null;

                var tokenObj = JsonConvert.DeserializeObject<GoogleTokenResponse>(tokenContent);
                var accessToken = tokenObj.access_token;

                // 2. Lấy thông tin user từ Google
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var userInfoResponse = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");
                var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();
                var userInfo = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(userInfoContent);

                var email = userInfo.GetProperty("email").GetString();
                var name = userInfo.GetProperty("name").GetString();

                // Lấy số điện thoại
                var peopleResponse = await httpClient.GetAsync("https://people.googleapis.com/v1/people/me?personFields=phoneNumbers");
                var peopleContent = await peopleResponse.Content.ReadAsStringAsync();
                var peopleJson = JsonDocument.Parse(peopleContent).RootElement;
                string? phoneNumber = null;
                if (peopleJson.TryGetProperty("phoneNumbers", out var phoneNumbers) && phoneNumbers.GetArrayLength() > 0)
                    phoneNumber = phoneNumbers[0].GetProperty("value").GetString();

                // 3. Tạo user nếu chưa có, hoặc lấy user từ DB
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                {

                    await _context.Users.AddAsync(new User
                    {
                        Email = email,
                        FullName = name,
                        Phone = phoneNumber,
                        Password = dateNow.Ticks + Guid.NewGuid().ToString(),
                        LastPwdChange = dateNow,
                        RoleId = 1,
                        IdentifyId = null,
                        Address = null,
                    });
                    await _context.SaveChangesAsync();
                    user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                }
                await transaction.CommitAsync();

                var userRefeshToken = new UserRefeshToken
                {
                    UserId = user.UserId,
                    RoleId = user.RoleId,
                    IdentifyId = user.IdentifyId,
                    FullName = user.FullName,
                    Address = user.Address,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = user.Password,
                    UserAgent = context.Request.Headers["User-Agent"].ToString(),
                    IPAddress = ipAddress,
                    LastPwdChange = user.LastPwdChange,
                };

                var token = await _refreshTokenRepository.GenerateTokenModel(userRefeshToken);
                StringBuilder url = new StringBuilder(_googleLoginSettings.ReturnAfterLogin);
                if (token is TokenModel)
                {
                    url.Append($"?AccessToken={token.AccessToken}&RefreshToken={token.RefreshToken}&roleId={userRefeshToken.RoleId}&userId={userRefeshToken.UserId}");
                }

                return url.ToString();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
    }
}

