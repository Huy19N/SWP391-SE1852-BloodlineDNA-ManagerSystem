using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultsController : ControllerBase
    {
        private readonly ITestResultRepository _testResultRepository;
        public TestResultsController(ITestResultRepository testResultRepository) => _testResultRepository = testResultRepository;

        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllTestResultPaging(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var TestResults = await Task.Run(() => _testResultRepository.GetAllTestResultsPaging(typeSearch, search, sortBy, page));
                if (TestResults == null)
                {
                    return NotFound();
                }
                return Ok(TestResults);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving TestResults:{ex.Message}");
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetTestResult(int id)
        {
            try
            {
                var TestResult = await Task.Run(() => _testResultRepository.GetTestResultsById(id));
                if (TestResult == null) return NotFound();
                return Ok(TestResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving test result: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public ActionResult CreateTestResult(TestResultDTO testResult)
        {
            try
            {
                if(testResult.Date == null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Date test result can not empty"
                    });
                }
                

                var isCreate = _testResultRepository.CreateTestResults(testResult);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetTestResult), new { id = testResult.ResultId }, testResult);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating TestResult: {ex.Message}");
            }


        }
        [HttpPut("Update")]
        public ActionResult UpdateTestResult(TestResultDTO testResult)
        {
            try
            {
                var isUpdate = _testResultRepository.UpdateTestResults(testResult);
                if (isUpdate)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update test result",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating test result: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteTestResult(int id)
        {
            try
            {
                var isDelete = _testResultRepository.DeleteTestResultsById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete test result",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting test result: {ex.Message}");
            }
        }
    }
}
