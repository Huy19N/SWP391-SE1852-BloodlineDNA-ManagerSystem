// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryMethodController : ControllerBase
    {
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;
        public DeliveryMethodController (IDeliveryMethodRepository deliveryMethodRepository) => _deliveryMethodRepository = deliveryMethodRepository;

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDeliveryMethod()
        {
            try
            {
                var data = await Task.Run(() => _deliveryMethodRepository.GetAllDeliveryMethods());
                if (data != null || !data.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found delivery method"
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all delivery method success",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error get all delivery method{ex.Message}");
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetDeliveryMethodById(int id)
        {
            try
            {
                var data = await Task.Run(() => _deliveryMethodRepository.GetDeliveryMethodById(id));
                if (data == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found delivery method",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get delivery method by id success",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving delivery method: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public ActionResult CreateDeliveryMethod(DeliveryMethodDTO deliveryMethod)
        {
            try
            {
                var isCreate = _deliveryMethodRepository.CreateDeliveryMethod(deliveryMethod);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetDeliveryMethodById), new { id = deliveryMethod.DeliveryMethodId }, deliveryMethod);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating delivery method: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public ActionResult UpdateDeliveryMethod(DeliveryMethodDTO deliveryMethod)
        {
            try
            {
                var isUpdate = _deliveryMethodRepository.UpdateDeliveryMethod(deliveryMethod);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found to update delivery method",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating delivery method: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteDeliveryMethodById(int id)
        {
            try
            {
                var isDelete = _deliveryMethodRepository.DeleteDeliveryMethodById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete delivery method",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting delivery method: {ex.Message}");
            }
        }
    }
}
