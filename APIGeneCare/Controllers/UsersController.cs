using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGeneCare.Data;
using APIGeneCare.Repository.Interface;
using APIGeneCare.Model;
using APIGeneCare.Repository;
using Microsoft.Build.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse>> Validate(LoginModel model)
        {

            try
            {
                var user = await Task.Run(() => _userRepository.Validate(model));
                if (user == null)
                {
                    return Unauthorized(new ApiResponse
                    {
                        Success = false,
                        Message = "Unauthorized user",
                    });
                }
                var Token =await Task.Run(()=> _userRepository.GenerateToken(user));

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Authentication Success",
                    Data = Token
                });
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error get Validate: {ex.Message}");
            }
        }
        // GET: api/Users
        //Retrieves a list of all users.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var users = await Task.Run(() => _userRepository.GetAllUsersPaging( typeSearch, search, sortBy, page));
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
                    Message = "Get All User Success",
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
        public async Task<ActionResult<User>> GetUserById(int id)
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
        public async Task<ActionResult<User>> GetUserByEmail(string? email)
        {
            try
            {
                User user = await Task.Run(() => _userRepository.GetUserByEmail(email ?? null!)) ?? null!;
                if (user == null)
                {
                    return NotFound(new ApiResponse 
                    { 
                        Success = false,
                        Message = "Get user by email error",
                        Data= null
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get user by email success",
                    Data = user
                });
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,$"Error retrieving user by email: {ex.Message}");
            }
        }
        // POST: api/Users
        //Creates a new user.
        [HttpPost("register")]
        public ActionResult CreateUser(RegisterModel registerModel)
        {
            try
            {
                var User = _userRepository.GetUserByEmail(registerModel.Email);
                if(User != null)
                {
                    return StatusCode(StatusCodes.Status302Found, new ApiResponse
                    {
                        Success = false,
                        Message = "The account having!"
                    });
                }
                var user = new User
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

        // put: api/Users/id
        //Updates a specific user by ID.
        [HttpPut("update/{id}")]
        public ActionResult UpdateUser(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "What are you doing?",
                    Data = null
                });

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

        // DELETE: api/Users/id
        //Deletes a specific user by ID.
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var isDelete = _userRepository.DeleteUser(id);
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

        [HttpPost("sendverifyemail")]
        public async Task<ActionResult<ApiResponse>> SendVerifyEmail(string email, string apiConfirmEmail)
        {
            try
            {
                var isSend = await _userRepository.SendConfirmEmail(email, apiConfirmEmail);
                if (!isSend)
                {
                    return Ok(new ApiResponse
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
        [HttpGet("confirmEmail")]
        public ActionResult ConfirmEmail(string email, string key) 
        {
            try
            {
                var verifyEmail = _userRepository.GetVerifyEmailByEmail(email);
                if (verifyEmail == null)
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "not found verify email"
                    });
                
                var isConfirm = _userRepository.ConfirmEmail(email, key);
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
    }
}