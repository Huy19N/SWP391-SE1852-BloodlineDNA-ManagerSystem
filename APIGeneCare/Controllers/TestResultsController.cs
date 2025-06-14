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
    public class TestResultsController : ControllerBase
    {
        private readonly ITestResultRepository _testResultRepository;
        public TestResultsController(ITestResultRepository testResultRepository) => _testResultRepository = testResultRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestResult>>> GetAllTestResultPaging(
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

        [HttpGet("{id}")]
        public async Task<ActionResult<TestResult>> GetTestResult(int id)
        {
            try
            {
                var TestResult = await Task.Run(() => _testResultRepository.GetTestResultsById(id));
                if (TestResult == null) return NotFound();
                return Ok(TestResult);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving test result: {ex.Message}");
            }
        }
        [HttpPost]
        public ActionResult CreateTestResult(TestResult testResult)
        {
            try
            {
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
        [HttpPut("{id}")]
        public ActionResult UpdateTestResult(int id, TestResult testResult) 
        {
            try
            {
                if (id != testResult.ResultId)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "What are you doing?",
                    Data = null
                });
            
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
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating test result: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTestResult(int id)
        {
            try
            {
                var isDelete = _testResultRepository.DeleteTestResults(id);
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
