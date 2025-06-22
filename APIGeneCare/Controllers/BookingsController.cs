// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingsController(IBookingRepository bookingRepository) => _bookingRepository = bookingRepository;

        [HttpGet("GetAllPaging")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookingsPaging(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var bookings = await Task.Run(() => _bookingRepository.GetAllBookingsPaging(typeSearch, search, sortBy, page));
                if (bookings == null || !bookings.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all booking failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all booking success",
                    Data = bookings
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all booking: {ex.Message}");
            }
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            try
            {
                var bookings = await Task.Run(() => _bookingRepository.GetAllBookings());
                if (bookings == null || !bookings.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all booking failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all booking success",
                    Data = bookings
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all booking: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            try
            {
                var booking = await Task.Run(() => _bookingRepository.GetBookingById(id));
                if (booking == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found booking",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get booking by id success",
                    Data = booking
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving booking: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public ActionResult CreateBooking(Booking booking)
        {
            try
            {
                var isCreate = _bookingRepository.CreateBooking(booking);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, booking);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating booking: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public ActionResult UpdateBooking(Booking booking)
        {
            try
            {
                var isUpdate = _bookingRepository.UpdateBooking(booking);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update booking",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating booking: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteBookingById(int id)
        {
            try
            {
                var isDelete = _bookingRepository.DeleteBookingById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete booking",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting booking: {ex.Message}");
            }
        }
    }
}
