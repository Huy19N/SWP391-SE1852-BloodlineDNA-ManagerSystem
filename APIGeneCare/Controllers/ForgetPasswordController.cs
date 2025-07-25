using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgetPasswordController : ControllerBase
    {
        private readonly IVerifyEmailRepository _verifyEmailRepository;
        public ForgetPasswordController(IVerifyEmailRepository verifyEmailRepository) => _verifyEmailRepository = verifyEmailRepository;

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

        [HttpPut("confirmForgetPassword")]
        public async Task<IActionResult> ConfirmForgetPassword(string email, string key, string password, string confirmPassword)
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

                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Password and confirm password is required"
                    });
                }

                if (password.Count() < 8)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Number characters of Password must more than or equar 8"
                    });
                }

                bool isConfirm = await _verifyEmailRepository.ConfirmForgetPassword(email, key, password);
                if (!isConfirm)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = "Key is not available"
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
        #region Set New Password
        //[HttpPut("SetNewPassword")]
        //public async Task<IActionResult> SetNewPassword(string currentPassword, string newPassword, string confirmNewPasword)
        //{
        //    try
        //    {

        //        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(key))
        //        {
        //            return BadRequest(new ApiResponse
        //            {
        //                Success = false,
        //                Message = "What are you doing?"
        //            });
        //        }

        //        if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPasword))
        //        {
        //            return BadRequest(new ApiResponse
        //            {
        //                Success = false,
        //                Message = "Password and confirm password is required"
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"error confirm forget password:{ex.Message}");
        //    }
        //}
        #endregion

    }
}
