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


        [HttpPost]
        public IActionResult Validate(LoginModel model)
        {

            try
            {
                var user = _userRepository.Validate(model);
                if (user == null)
                {
                    return Unauthorized(new ApiResponse
                    {
                        Success = false,
                        Message = "Unauthorized user",
                    });
                }
                var Token = _userRepository.GenerateToken(user);

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
                var users = await Task.Run(() => _userRepository.GetAllUsers( typeSearch, search, sortBy, page));
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
        [HttpGet("id")]
        public async Task<ActionResult<User>> GetUser(int id)
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving user: {ex.Message}");
            }
        }

        // POST: api/Users
        //Creates a new user.
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            try
            {
                var isCreate = _userRepository.CreateUser(user);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
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
        [HttpPut("{id}")]
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
        [HttpDelete("id")]
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

    }
}