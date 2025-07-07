using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        public BlogsController(IBlogRepository blogRepository) => _blogRepository = blogRepository;

        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllBlogsPaging(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var blogs = await Task.Run(() => _blogRepository.GetAllBlogsPaging(typeSearch, search, sortBy, page));
                if (blogs == null || !blogs.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all blog paging failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all blog paging success",
                    Data = blogs
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all blog: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            try
            {
                var blog = await Task.Run(() => _blogRepository.GetBlogById(id));
                if (blog == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Not found blog",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get blog by id success",
                    Data = blog
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving blog: {ex.Message}");
            }
        }
        [HttpPost("Create")]
        public IActionResult CreateBlog(BlogDTO blog)
        {
            try
            {
                var isCreate = _blogRepository.CreateBlog(blog);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetBlogById), new { id = blog.BlogId }, blog);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating blog: {ex.Message}");
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateBlog(BlogDTO blog)
        {
            try
            {
                var isUpdate = _blogRepository.UpdateBlog(blog);
                if (isUpdate)
                    return NoContent();
                else
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error update blog",
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating blog: {ex.Message}");
            }
        }
        [HttpDelete("DeleteById/{id}")]
        public IActionResult DeleteBlogById(int id)
        {
            try
            {
                var isDelete = _blogRepository.DeleteBlogById(id);
                if (isDelete)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "error delete blog",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting blog: {ex.Message}");
            }
        }
    }
}
