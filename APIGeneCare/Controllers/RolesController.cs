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
using System.Data;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RolesController(IRoleRepository roleRepository) => _roleRepository = roleRepository;

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRolesPaging(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var roles = await Task.Run(() => _roleRepository.GetAllRolesPaging(typeSearch, search, sortBy, page));
                if (roles == null || !roles.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all role failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all role success",
                    Data = roles
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all role: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            try
            {
                var role = await Task.Run(() => _roleRepository.GetRoleById(id));
                if (role == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found role",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get role by id success",
                    Data = role
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving role: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public ActionResult CreateRole(Role role)
        {
            try
            {
                var isCreate = _roleRepository.CreateRole(role);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleId }, role);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating role: {ex.Message}");
            }
        }
        [HttpPut("Update")]
        public ActionResult UpdateRole(Role role)
        {
            try
            {
                var isUpdate = _roleRepository.UpdateRole(role);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update role",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating role: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteRoleById(int id)
        {
            try
            {
                var isDelete = _roleRepository.DeleteRole(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete role",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting role: {ex.Message}");
            }
        }
    }
}
