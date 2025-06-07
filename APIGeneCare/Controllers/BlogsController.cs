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
using System.Reflection.Metadata;

namespace APIGeneCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        public BlogsController(IBlogRepository blogRepository) => _blogRepository = blogRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetAllBlogs(
            [FromQuery] string? typeSearch,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] int? page)
        {
            try
            {
                var blogs = await Task.Run(() => _blogRepository.GetAllBlogs(typeSearch, search, sortBy, page));
                if (blogs == null || !blogs.Any())
                {
                    return NotFound(new ApiResponse
                    {
                        Success = false,
                        Message = "Get all blog failed",
                        Data = null
                    });
                }
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Get all blog success",
                    Data = blogs
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all blog: {ex.Message}");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
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
        [HttpPost]
        public ActionResult CreateBlog(Blog blog)
        {
            try
            {
                var isCreate = _blogRepository.CreateBlog(blog);
                if (isCreate)
                {
                    return CreatedAtAction(nameof(GetBlog), new { id = blog.BlogId }, blog);
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

        [HttpPut("{id}")]
        public ActionResult UpdateBlog(int id, Blog blog)
        {
            try
            {
                if (id != blog.BlogId)
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "What are you doing?",
                        Data = null
                    });

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
        [HttpDelete("id")]
        public ActionResult DeleteBlog(int id)
        {
            try
            {
                var isDelete = _blogRepository.DeleteBlog(id);
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
