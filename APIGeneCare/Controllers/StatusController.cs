// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;
        public StatusController(IStatusRepository statusRepository) => _statusRepository = statusRepository;

        [HttpGet("GetAllStatus")]
        public async Task<IActionResult> GetAllStatus()
        {
            try
            {
                var statuses = await Task.Run(() => _statusRepository.GetAllStatus());
                if (statuses == null || !statuses.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all status failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all status success",
                    Data = statuses
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all status: {ex.Message}");
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            try
            {
                var status = await Task.Run(() => _statusRepository.GetStatusById(id));
                if (status == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found status",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get status by id success",
                    Data = status
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving status: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public ActionResult CreateStatus(StatusDTO status)
        {
            try
            {
                var isCreate = _statusRepository.CreateStatus(status);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetStatusById), new { id = status.StatusId }, status);
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Create status failed",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating status: {ex.Message}");
            }
        }
        [HttpPut("Update")]
        public ActionResult UpdateStatus(StatusDTO status)
        {
            try
            {
                var isUpdate = _statusRepository.UpdateStatus(status);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Update status failed",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating status: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteStatusById(int id)
        {
            try
            {
                var isDelete = _statusRepository.DeleteStatusById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Delete status failed",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting status: {ex.Message}");
            }
        }
    }
}
