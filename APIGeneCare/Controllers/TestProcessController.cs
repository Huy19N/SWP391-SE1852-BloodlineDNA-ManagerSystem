using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestProcessController : ControllerBase
    {
        private readonly ITestProcessRepository _testProcessRepository;
        public TestProcessController(ITestProcessRepository testProcessRepository)
        {
            _testProcessRepository = testProcessRepository;
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllTestProcessesPaging(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var testProcesses = await Task.Run(() =>
                    _testProcessRepository.GetAllTestProcessPaging(typeSearch, search, sortBy, page));

                if (testProcesses == null || !testProcesses.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "No Test Processes found",
                        Data = null
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get All Test Processes Successful",
                    Data = testProcesses
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing your request: {ex.Message}");
            }
        }
        // GET: api/TestProcess/GetById/{id}
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetTestProcessById(int id)
        {
            try
            {
                var testProcess = await Task.Run(() => _testProcessRepository.GetTestProcessById(id));

                if (testProcess == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Test Process not found",
                        Data = null
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get Test Process by Id Successful",
                    Data = testProcess
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the Test Process: {ex.Message}");
            }
        }
        // POST: api/TestProcess/Create
        [HttpPost("Create")]
        public ActionResult CreateTestProcess(TestProcessDTO testProcess)
        {
            try
            {
                var isCreated = _testProcessRepository.CreateTestProcess(testProcess);

                if (isCreated)
                {
                    return CreatedAtAction(nameof(GetTestProcessById), new { id = testProcess.ProcessId }, testProcess);
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Could not create Test Process",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the Test Process: {ex.Message}");
            }
        }
        // PUT: api/TestProcess/Update
        [HttpPut("Update")]
        public ActionResult UpdateTestProcess(TestProcessDTO testProcess)
        {
            try
            {
                var isUpdated = _testProcessRepository.UpdateTestProcess(testProcess);

                if (isUpdated)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Could not update Test Process",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the Test Process: {ex.Message}");
            }
        }
    }
}
