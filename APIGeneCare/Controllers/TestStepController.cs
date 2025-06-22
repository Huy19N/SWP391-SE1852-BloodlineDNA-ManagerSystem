// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestStepController : ControllerBase
    {
        private readonly ITestStepRepository _testStepRepository;
        public TestStepController(ITestStepRepository testStepRepository) => _testStepRepository = testStepRepository;
        [HttpGet("getAllTestSteps")]
        public async Task<IActionResult> GetAllTestSteps()
        {
            try
            {
                var testSteps = await Task.Run(() => _testStepRepository.GetAllTestStep());
                if (testSteps == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found test step",
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Found!",
                    Data = testSteps
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error get all test step: {ex.Message}");
            }
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetTestStepById(int id)
        {
            try
            {
                var testStep = await Task.Run(() => _testStepRepository.GetTestStepById(id));
                if (testStep == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found test step",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get test step by id success",
                    Data = testStep
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving test step by id: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public ActionResult CreateUser(TestStep testStep)
        {
            try
            {

                var isCreate = _testStepRepository.CreateTestStep(testStep);
                if (isCreate)
                {

                    return CreatedAtAction(nameof(GetTestStepById), new { id = testStep.StepId }, testStep);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating test step: {ex.Message}");
            }
        }
        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTestStep(TestStep testStep)
        {
            try
            {
                var isUpdate = await Task.Run(() => _testStepRepository.UpdateTestStep(testStep));
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update test step",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating test step: {ex.Message}");
            }
        }

        [HttpDelete("DeleteById/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTestStepById(int id)
        {
            try
            {
                var isDelete = _testStepRepository.DeleteTestStepById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found to delete test step",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting test step: {ex.Message}");
            }
        }
    }
}
