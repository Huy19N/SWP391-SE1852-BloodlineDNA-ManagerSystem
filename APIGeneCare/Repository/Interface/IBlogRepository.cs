// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;

namespace APIGeneCare.Repository.Interface
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> GetAllBlogsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        Blog? GetBlogById(int id);
        bool CreateBlog(Blog blog);
        bool UpdateBlog(Blog blog);
        bool DeleteBlogById(int id);
    }
}
