using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGeneCare.Data;
using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        public ServicesController(IServiceRepository serviceRepository) => _serviceRepository = serviceRepository;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var services = await Task.Run(() => _serviceRepository.GetAllServices(typeSearch, search, sortBy, page));
                if (services == null || !services.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all services failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get All User Success",
                    Data = services
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all services: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetService(int id)
        {
            try
            {
                var service = await Task.Run(() => _serviceRepository.GetServiceById(id));
                if (service == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found service",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get service by id success",
                    Data = service
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving service: {ex.Message}");
            }
        }
        [HttpPost]
        public ActionResult CreateService(Service service)
        {
            try
            {
                var isCreate = _serviceRepository.CreateService(service);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetService), new { id = service.ServiceId }, service);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating service: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, Service service)
        {
            try
            {
                if (id != service.ServiceId)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "What are you doing?",
                    Data = null
                });

            
                var isUpdate = _serviceRepository.UpdateService(service);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update Service",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating service: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteService(int id)
        {
            try
            {
                var isDelete = _serviceRepository.DeleteService(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete Service",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting Service: {ex.Message}");
            }
        }
    }
}
