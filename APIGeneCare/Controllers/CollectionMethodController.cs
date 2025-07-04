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
    public class CollectionMethodController : ControllerBase
    {
        private readonly ICollectionMethodRepository _collectionMethodRepository;
        public CollectionMethodController(ICollectionMethodRepository collectionMethodRepository) => _collectionMethodRepository = collectionMethodRepository;

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCollectionMethod() 
        {
            try
            {
                var data = await Task.Run(() => _collectionMethodRepository.GetAllCollectionMethods());
                if(data == null || !data.Any()){
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found get all collection method"
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all collection method success",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error get all collection method{ex.Message}");
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCollectionMethodById(int id)
        {
            try
            {
                var data = await Task.Run(() => _collectionMethodRepository.GetCollectionMethodById(id));
                if (data == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found collection method",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get collection method by id success",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving collection method: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public ActionResult CreateCollectionMethod(CollectionMethodDTO collectionMethod)
        {
            try
            {
                var isCreate = _collectionMethodRepository.CreateCollectionMethod(collectionMethod);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetCollectionMethodById), new { id = collectionMethod.MethodId}, collectionMethod);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating collection method: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public ActionResult UpdateCollectionMethod(CollectionMethodDTO collectionMethod)
        {
            try
            {
                var isUpdate = _collectionMethodRepository.UpdateCollectionMethod(collectionMethod);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found to update collection method",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating collection method: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public ActionResult DeleteCollectionMethodById(int id)
        {
            try
            {
                var isDelete = _collectionMethodRepository.DeleteCollectionMethodById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete collection method",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting collection method: {ex.Message}");
            }
        }
    }
}
