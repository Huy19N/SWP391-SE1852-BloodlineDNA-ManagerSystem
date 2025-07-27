using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        public PatientController(IPatientRepository patientRepository) => _patientRepository = patientRepository;
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPatients()
        {
            try
            {
                var patients = await Task.Run(() => _patientRepository.GetAllPatients());
                if (!patients.Any() && patients == null)
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all patients failed!"
                    });
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Get all patients!",
                    Data = patients
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all patient: {ex.Message}");
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            try
            {
                var patient = await Task.Run(() => _patientRepository.GetPatientById(id));
                if (patient == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found patient",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get patient by id success",
                    Data = patient
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving patient: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public IActionResult CreatePatient(PatientDTO patient)
        {
            try
            {
                var isCreate = _patientRepository.CreatePatient(patient);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetPatientById), new { id = patient.PatientId }, patient);
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
        [HttpPost("CreatePatientWithBooking")]
        public async Task<IActionResult> CreatePatientWithBooking(BookingWithPatient bookingWithPatient)
        {
            try
            {
                foreach(var x in bookingWithPatient.patients)
                {
                    if (DateTime.Parse(x.BirthDate.ToString()) >= DateTime.Now)
                        return BadRequest(new ApiResponse
                        {
                            Success = false,
                            Message = "A person in this form have birthday greater than now!"
                        });
                    if (string.IsNullOrWhiteSpace(x.FullName) || !Regex.IsMatch(x.FullName, @"\d"))
                        return BadRequest(new ApiResponse
                        {
                            Success = false,
                            Message = "Name must not empty and not contain number"
                        });
                    if (string.IsNullOrEmpty(x.Relationship) || !Regex.IsMatch(x.Relationship, @"\d"))
                        return BadRequest(new ApiResponse
                        {
                            Success = false,
                            Message = "Relationship must not empty and not contain number"
                        });

                }
                var bookingId = await _patientRepository.CreatePatientWithBookingAsync(bookingWithPatient);
                if (bookingId > 0)
                {
                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Create Successful",
                        Data = new { bookingId }
                    });
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
        public IActionResult UpdatePatient(PatientDTO patient)
        {
            try
            {
                var isUpdate = _patientRepository.UpdatePatient(patient);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update patient",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating patient: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public IActionResult DeletePatientById(int id)
        {
            try
            {
                var isDelete = _patientRepository.DeletePatientById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete patient",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting patient: {ex.Message}");
            }
        }
    }
}
