using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public BlogRepository(GeneCareContext context) => _context = context;
        public IEnumerable<BlogDTO> GetAllBlogsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allBlogs = _context.Blogs.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("blogid", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int blogid))
                        allBlogs = _context.Blogs.Where(b => b.BlogId == blogid);

                if (typeSearch.Equals("userid", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int userid))
                        allBlogs = _context.Blogs.Where(b => b.UserId == userid);

                if (typeSearch.Equals("title", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = _context.Blogs.Where(b => !String.IsNullOrWhiteSpace(b.Title) &&
                    b.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                if (typeSearch.Equals("content", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = _context.Blogs.Where(b => !String.IsNullOrWhiteSpace(b.Content) &&
                    b.Content.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            }
            #endregion
            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("blogid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderBy(b => b.BlogId);

                if (sortBy.Equals("blogid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderByDescending(b => b.BlogId);

                if (sortBy.Equals("userid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderBy(b => b.UserId);

                if (sortBy.Equals("userid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderByDescending(b => b.UserId);

                if (sortBy.Equals("title_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderBy(b => b.Title);

                if (sortBy.Equals("title_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderByDescending(b => b.Title);

                if (sortBy.Equals("content_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderBy(b => b.Content);

                if (sortBy.Equals("content_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderByDescending(b => b.Content);

                if (sortBy.Equals("createdat_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderBy(b => b.CreatedAt);

                if (sortBy.Equals("createdat_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBlogs = allBlogs.OrderByDescending(b => b.CreatedAt);
            }
            #endregion

            var result = PaginatedList<Blog>.Create(allBlogs, page ?? 1, PAGE_SIZE);
            return result.Select(b => new BlogDTO
            {
                BlogId = b.BlogId,
                UserId = b.UserId,
                Title = b.Title,
                Content = b.Content,
                CreatedAt = b.CreatedAt
            });
        }
        public IEnumerable<BlogDTO> GetAllBlogs()
            => _context.Blogs.Select(b => new BlogDTO
            {
                BlogId = b.BlogId,
                UserId = b.UserId,
                Title = b.Title,
                Content = b.Content,
                CreatedAt = b.CreatedAt
            }).ToList();
        public BlogDTO? GetBlogById(int id)
            => _context.Blogs.Select(b => new BlogDTO
            {
                BlogId = b.BlogId,
                UserId = b.UserId,
                Title = b.Title,
                Content = b.Content,
                CreatedAt = b.CreatedAt
            }).FirstOrDefault(b => b.BlogId == id);
        public bool CreateBlog(BlogDTO blog)
        {
            if (blog == null) return false;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Blogs.Add(new Blog()
                {
                    Content = blog.Content,
                    CreatedAt = DateTime.Now,
                    Title = blog.Title,
                    UserId = blog.UserId,
                });
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool UpdateBlog(BlogDTO blog)
        {
            if (blog == null) return false;
            var existingBlog = _context.Blogs.Find(blog.BlogId);
            if (existingBlog == null) return false;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existingBlog.UserId = blog.UserId;
                existingBlog.Title = blog.Title;
                existingBlog.Content = blog.Content;
                existingBlog.CreatedAt = blog.CreatedAt;

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool DeleteBlogById(int id)
        {
            var blog = _context.Blogs.Find(id);
            if (blog == null) return false;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
