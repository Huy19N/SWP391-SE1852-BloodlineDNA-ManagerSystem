using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository) => _userRepository = userRepository;

        [HttpPost("Login")]
        public async Task<IActionResult> Validate(LoginModel loginModel)
        {
            try
            {
                var model = await Task.Run(() => _userRepository.Login(loginModel, HttpContext));
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

                    return CreatedAtAction(nameof(GetUserByEmail), new { email = registerModel.Email }, registerModel);
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
        // GET: api/Users
        //Retrieves a list of all users.
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllUsersPaging(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var users = await Task.Run(() => _userRepository.GetAllUsersPaging(typeSearch, search, sortBy, page));
                if (users == null || !users.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all user failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all user paging success",
                    Data = users
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving users: {ex.Message}");
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await Task.Run(() => _userRepository.GetAllUsers());
                if (users == null || !users.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all user failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all user success",
                    Data = users
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving users: {ex.Message}");
            }
        }
        // GET: api/Users/id
        //Retrieves a specific user by ID.
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await Task.Run(() => _userRepository.GetUserById(id));
                if (user == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found user",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get user by id success",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving user by id: {ex.Message}");
            }
        }
        [HttpGet("getbyemail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string? email)
        {
            try
            {
                UserDTO user = await Task.Run(() => _userRepository.GetUserByEmail(email ?? null!)) ?? null!;
                if (user == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get user by email error",
                        Data = null
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get user by email success",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving user by email: {ex.Message}");
            }
        }
        // put: api/Users/id
        //Updates a specific user by ID.
        [HttpPut("Update/{id}")]
        public IActionResult UpdateUser(UserDTO user)
        {
            try
            {
                var isUpdate = _userRepository.UpdateUser(user);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update user",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user: {ex.Message}");
            }
        }

        //[HttpPut("ForgetPasword")]
        //public async Task<IActionResult> ForgetPassword(string email)
        //{
        //    try
        //    {
        //        if (resetPasswordModel == null || string.IsNullOrEmpty(resetPasswordModel.Email) ||
        //            string.IsNullOrEmpty(resetPasswordModel.Password) ||
        //            string.IsNullOrEmpty(resetPasswordModel.ConfirmPassword))
        //        {
        //            return BadRequest(new ApiResponse
        //            {
        //                Success = false,
        //                Message = "Invalid reset password request",
        //                Data = null
        //            });
        //        }
        //        var isReset = _userRepository.ResetPassword(resetPasswordModel);
        //        if (isReset)
        //        {
        //            return Ok(new ApiResponse
        //            {
        //                Success = true,
        //                Message = "Password reset successfully",
        //                Data = null
        //            });
        //        }
        //        else
        //        {
        //            return NotFound(new ApiResponse
        //            {
        //                Success = false,
        //                Message = "User not found or old password is incorrect",
        //                Data = null
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error resetting password: {ex.Message}");
        //    }
        //}
        // DELETE: api/Users/id
        //Deletes a specific user by ID.
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var isDelete = _userRepository.DeleteUserById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete user",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting user: {ex.Message}");
            }
        }
    }
}