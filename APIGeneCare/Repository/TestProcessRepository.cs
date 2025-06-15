using APIGeneCare.Data;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class TestProcessRepository : ITestProcessRepository
    {
        public bool CreateTestProcess(TestProcess testProcess)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTestProcessById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TestProcess> GetAllBlogsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TestProcess> GetAllTest()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TestProcess> GetAllTestProcessPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public TestProcess? GetBlogById(int id)
        {
            throw new NotImplementedException();
        }

        public TestProcess? GetTestProcessById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTestProcess(TestProcess testProcess)
        {
            throw new NotImplementedException();
        }
    }
}
