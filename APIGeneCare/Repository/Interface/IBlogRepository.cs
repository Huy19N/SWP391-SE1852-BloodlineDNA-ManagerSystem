using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> GetAllBlogsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        Blog? GetBlogById(int id);
        bool CreateBlog(Blog blog);
        bool UpdateBlog(Blog blog);
        bool DeleteBlog(int id);
    }
}
