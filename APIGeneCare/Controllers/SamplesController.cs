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
    public class SamplesController : ControllerBase
    {
        private readonly ISampleRepository _sampleRepository;

        public SamplesController(ISampleRepository sampleRepository) => _sampleRepository = sampleRepository;

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<Sample>>> GetAllSamplesPaging(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var samples = await Task.Run(() => _sampleRepository.GetAllSamplesPaging(typeSearch, search, sortBy, page));
                if (samples == null || !samples.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all all sample failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all sample success",
                    Data = samples
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all sample: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Sample>> GetSampleById(int id)
        {
            try
            {
                var sample = await Task.Run(() => _sampleRepository.GetSampleById(id));
                if (sample == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found sample",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get sample by id success",
                    Data = sample
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving sample: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public ActionResult CreateSample(Sample sample)
        {
            try
            {
                var isCreate = _sampleRepository.CreateSample(sample);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetSampleById), new { id = sample.SampleId }, sample);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating sample: {ex.Message}");
            }
        }
        [HttpPut("Update")]
        public ActionResult UpdateSample(Sample sample)
        {
            try
            {
                var isUpdate = _sampleRepository.UpdateSample(sample);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update sample",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating sample: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteSampleById(int id)
        {
            try
            {
                var isDelete = _sampleRepository.DeleteSample(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete sample",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting sample: {ex.Message}");
            }
        }
    }
}
