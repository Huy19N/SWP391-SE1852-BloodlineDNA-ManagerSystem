using APIGeneCare.Model;
using APIGeneCare.Model.AppSettings;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthRepository _authRepository;
        private readonly GoogleLoginSettings _googleLoginSettings;
        public AuthController(IUserRepository userRepository,
            IAuthRepository authRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IOptionsMonitor<GoogleLoginSettings> googleLoginSettings)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _googleLoginSettings = googleLoginSettings.CurrentValue;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Validate(LoginModel loginModel)
        {
            try
            {
                var model = await Task.Run(() => _authRepository.Login(loginModel, HttpContext));
                if (model == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found user!",
                    });
                }
                if (model is LockResponseModel)
                {
                    return StatusCode(StatusCodes.Status423Locked, model);
                }

                if (model is TokenModel)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Login response",
                        Data = model
                    });
                }
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "What are you doing?",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error get Validate: {ex.Message}");
            }
        }

        // POST: api/Users
        //Creates a new user.
        [HttpPost("Register")]
        public ActionResult CreateUser(RegisterModel registerModel)
        {
            try
            {
                var User = _userRepository.GetUserByEmail(registerModel.Email);
                if (User != null)
                {
                    return StatusCode(StatusCodes.Status302Found, new ApiResponse
                    {
                        Success = false,
                        Message = "The account having!"
                    });
                }
                if (!registerModel.ConfirmPassword.Equals(registerModel.Password))
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "password or confirm password do not match"
                    });
                if (String.IsNullOrEmpty(registerModel.Password) ||
                    String.IsNullOrEmpty(registerModel.ConfirmPassword))
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "password or confirm password can not empty"
                    });
                var user = new UserDTO
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    RoleId = 1
                };
                var isCreate = _userRepository.CreateUser(user);
                if (isCreate)
                {

                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Create user successfully",
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "What are you doing?",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating user: {ex.Message}");
            }
        }

        [HttpGet("GoogleLogin")]
        public IActionResult GetGoogleLoginUrl()
        {
            try
            {
                var url = _authRepository.GenerateUrlGoogleLogin();
                if (string.IsNullOrEmpty(url))
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Failed to generate Google login URL."
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Google login URL generated successfully.",
                    Data = url
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while generating the Google login URL: {ex.Message}");
            }
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code, [FromQuery] string state)
        {
            try
            {
                var tokenModel = await _authRepository.GoogleLoginCallback(code, state, HttpContext);
                if (tokenModel == null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Failed to authenticate with Google."
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Google login successful.",
                    Data = tokenModel
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred during Google login callback: {ex.Message}");
            }
        }

        [HttpGet("GetAccessToken")]
        public async Task<IActionResult> GetAccessToken([FromQuery] string refreshToken)
        {
            try
            {
                var token = await _refreshTokenRepository.GenerateAccessTokenByRefToken(refreshToken);
                if (token == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token not found or invalid."
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Access token retrieved successfully.",
                    Data = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the access token: {ex.Message}");
            }
        }
    }
}
