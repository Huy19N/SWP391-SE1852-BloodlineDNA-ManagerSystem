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
        public async Task<IActionResult> ConfirmEmail(string email, string otp)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(otp))
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "What are you doing?"
                    });
                }

                if (otp.Trim().Length < 6) 
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "OTP must have 6 characters!"
                    });
                }
                var verifyEmail = _verifyEmailRepository.GetVerifyEmailByEmail(email, otp);
                if (verifyEmail == null)
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found OTP"
                    });
                bool isConfirm = await _verifyEmailRepository.ConfirmEmail(email, otp);
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

    }
}
