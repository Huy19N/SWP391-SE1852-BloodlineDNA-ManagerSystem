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
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingsController(IBookingRepository bookingRepository) => _bookingRepository = bookingRepository;

        [HttpGet]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
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
        [HttpPost]
        public ActionResult CreateBooking(Booking booking)
        {
            try
            {
                var isCreate = _bookingRepository.CreateBooking(booking);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingId }, booking);
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

        [HttpPut("{id}")]
        public ActionResult UpdateBooking(int id, Booking booking)
        {
            try
            {
                if (id != booking.BookingId)
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "What are you doing?",
                        Data = null
                    });

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
        [HttpDelete("{id}")]
        public ActionResult DeleteBooking(int id)
        {
            try
            {
                var isDelete = _bookingRepository.DeleteBooking(id);
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
