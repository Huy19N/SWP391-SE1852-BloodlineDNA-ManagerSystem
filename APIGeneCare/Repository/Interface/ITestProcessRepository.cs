using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface ITestProcessRepository
    {
        IEnumerable<TestProcess> GetAllTestProcessPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<TestProcess> GetAllTest();
        TestProcess? GetTestProcessById(int id);
        bool CreateTestProcess(TestProcess testProcess);
        bool UpdateTestProcess(TestProcess testProcess);
        bool DeleteTestProcessById(int id);
    }
}
