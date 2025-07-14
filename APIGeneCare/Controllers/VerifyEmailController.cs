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


        #region VerifyEmail
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

                var isSend = await _verifyEmailRepository.SendConfirmEmail(email);
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

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string key)
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
                bool isConfirm = await _verifyEmailRepository.ConfirmEmail(email, key);
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
        #endregion

        #region Forget Password
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

                var isSend = await _verifyEmailRepository.SendEmailConfirmForgetPassword(email);
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
        public async Task<IActionResult> ConfirmForgetPassword(string email, string key)
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
                bool isConfirm = await _verifyEmailRepository.ConfirmEmail(email, key);
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
        #endregion
    }
}
