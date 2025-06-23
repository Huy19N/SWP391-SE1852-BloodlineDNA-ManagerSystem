// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IBlogRepository
    {
        IEnumerable<BlogDTO> GetAllBlogsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<BlogDTO> GetAllBlogs();
        BlogDTO? GetBlogById(int id);
        bool CreateBlog(BlogDTO blog);
        bool UpdateBlog(BlogDTO blog);
        bool DeleteBlogById(int id);
    }
}
