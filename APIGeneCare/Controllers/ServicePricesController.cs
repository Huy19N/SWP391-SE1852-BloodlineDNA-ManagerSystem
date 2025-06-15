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
    public class ServicePricesController : ControllerBase
    {
        private readonly IServicePriceRepository _servicePriceRepository;

        public ServicePricesController(IServicePriceRepository servicePriceRepository) => _servicePriceRepository = servicePriceRepository;

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<ServicePrice>>> GetAllServicePricesPaging(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var servicePrices = await Task.Run(() => _servicePriceRepository.GetAllServicePricesPaging(
                    typeSearch, search, 
                    sortBy, page));
                if (servicePrices == null || !servicePrices.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all service price failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all service price Success",
                    Data = servicePrices
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all service price: {ex.Message}");
            }
        }

        
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<User>> GetServicePriceById(int id)
        {
            try
            {
                var servicePrice = await Task.Run(() => _servicePriceRepository.GetServicePriceById(id));
                if (servicePrice == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found service price",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get service price by id success",
                    Data = servicePrice
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving service price: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public ActionResult CreateServicePrice(ServicePrice servicePrice)
        {
            try
            {
                var isCreate = _servicePriceRepository.CreateServicePrice(servicePrice);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetServicePriceById), new { id = servicePrice.PriceId }, servicePrice);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating service price: {ex.Message}");
            }
        }
        [HttpPut("Update")]
        public ActionResult UpdateUser(ServicePrice servicePrice)
        {
            try
            {
                var isUpdate = _servicePriceRepository.UpdateServicePrice(servicePrice);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update service price",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating service price: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var isDelete = _servicePriceRepository.DeleteServicePriceById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete service price",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting service price: {ex.Message}");
            }
        }
    }
}
