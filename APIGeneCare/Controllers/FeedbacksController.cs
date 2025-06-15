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
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbacksController(IFeedbackRepository feedbackRepository) => _feedbackRepository = feedbackRepository;
        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetAllFeedbacksPaging(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var feedbacks = await Task.Run(() => _feedbackRepository.GetAllFeedbacksPaging(typeSearch, search, sortBy, page));
                if (feedbacks == null || !feedbacks.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all feedback failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all feedback success",
                    Data = feedbacks
                }); 

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all feedback: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Feedback>> GetFeedbackById(int id)
        {
            try
            {
                var feedback = await Task.Run(() => _feedbackRepository.GetFeedbackById(id));
                if (feedback == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found feedback",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get feedback by id success",
                    Data = feedback
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving feedback: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public ActionResult CreateFeedback(Feedback feedback)
        {
            try
            {
                var isCreate = _feedbackRepository.CreateFeedback(feedback);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetFeedbackById), new { id = feedback.FeedbackId }, feedback);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating feedback: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public ActionResult UpdateFeedback(Feedback feedback)
        {
            try
            {
                var isUpdate = _feedbackRepository.UpdateFeedback(feedback);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update feedback",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating feedback: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteFeedbackById(int id)
        {
            try
            {
                var isDelete = _feedbackRepository.DeleteFeedback(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete feedback",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting feedback: {ex.Message}");
            }
        }
    }
}
