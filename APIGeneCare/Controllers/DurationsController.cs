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

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DurationsController : ControllerBase
    {
        private readonly IDurationRepository _durationRepository;
        public DurationsController(IDurationRepository durationRepository) => _durationRepository = durationRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Duration>>> GetAllDurations(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var durations = await Task.Run(() => _durationRepository.GetAllDurations(typeSearch, search, sortBy, page));
                if (durations == null || !durations.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all duration failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all duration success",
                    Data = durations
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all duration: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Duration>> GetDuration(int id)
        {
            try
            {
                var duration = await Task.Run(() => _durationRepository.GetDurationById(id));
                if (duration == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found duration",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get duration by id success",
                    Data = duration
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving duration: {ex.Message}");
            }
        }
        [HttpPost]
        public ActionResult CreateDuration(Duration duration)
        {
            try
            {
                var isCreate = _durationRepository.CreateDuration(duration);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetDuration), new { id = duration.DurationId }, duration);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating duration: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateDuration(int id, Duration duration)
        {
            try
            {
                if (id != duration.DurationId)
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "What are you doing?",
                        Data = null
                    });

                var isUpdate = _durationRepository.UpdateDuration(duration);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update duration",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating duration: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteDuration(int id)
        {
            try
            {
                var isDelete = _durationRepository.DeleteDuration(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete duration",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting duration: {ex.Message}");
            }
        }
    }
}
