using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface ITestProcessRepository
    {
        IEnumerable<TestProcess> GetAllBlogsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        TestProcess? GetBlogById(int id);
        bool CreateTestProcess(TestProcess testProcess);
        bool UpdateTestProcess(TestProcess testProcess);
        bool DeleteTestProcessById(int id);
    }
}
