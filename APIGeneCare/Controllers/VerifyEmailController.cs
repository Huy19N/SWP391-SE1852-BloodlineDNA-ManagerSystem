using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyEmailController : ControllerBase
    {
        private readonly IVerifyEmailRepository _verifyEmailRepository;
        public VerifyEmailController(IVerifyEmailRepository verifyEmailRepository) => _verifyEmailRepository = verifyEmailRepository;
        [HttpPost("SendVerifyEmail")]
        public async Task<IActionResult> SendVerifyEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Email is required"
                    });

                var baseUrl = GetBaseUrl();
                var apiConfirmEmail = $"{baseUrl}/api/VerifyEmail/confirmEmail?";

                var isSend = await _verifyEmailRepository.SendConfirmEmail(email, apiConfirmEmail);
                if (!isSend)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Email can't send"
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Email have been send"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error send email:{ex.Message}");
            }
        }
        [HttpPost("SendEmailConfirmForgetPassword")]
        public async Task<IActionResult> SendEmailConfirmForgetPassword(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Email is required"
                    });

                var baseUrl = GetBaseUrl();
                var apiConfirmEmail = $"{baseUrl}/api/VerifyEmail/confirmEmail?";

                var isSend = await _verifyEmailRepository.SendEmailConfirmForgetPassword(email, apiConfirmEmail);
                if (!isSend)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Email can't send"
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Email have been send"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error send email:{ex.Message}");
            }
        }

        [HttpGet("confirmForgetPassword")]
        public IActionResult ConfirmForgetPassword(string email, string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(key))
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "What are you doing?"
                    });
                }
                var verifyEmail = _verifyEmailRepository.GetVerifyEmailByEmail(email);
                if (verifyEmail == null)
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "not found verify email"
                    });
                if (verifyEmail.Key?.ToLower() != key.ToLower())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "The key not match!"
                    });
                }
                bool isConfirm = _verifyEmailRepository.ConfirmEmail(email, key);
                if (!isConfirm)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Link expired"
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Confirm forget password success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"error confirm forget password:{ex.Message}");
            }
        }

        [HttpGet("ConfirmEmail")]
        public ActionResult ConfirmEmail(string email, string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(key))
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "What are you doing?"
                    });
                }
                var verifyEmail = _verifyEmailRepository.GetVerifyEmailByEmail(email);
                if (verifyEmail == null)
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "not found verify email"
                    });
                if (verifyEmail.Key?.ToLower() != key.ToLower())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "The key not match!"
                    });
                }
                bool isConfirm = _verifyEmailRepository.ConfirmEmail(email, key);
                if (!isConfirm)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Link expired"
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Confirm email success"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"error confirm email:{ex.Message}");
            }
        }

        private string GetBaseUrl()
        {
            var request = HttpContext.Request;
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }
}
