using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface ITestProcessRepository
    {
        IEnumerable<TestProcessDTO> GetAllTestProcessPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<TestProcessDTO> GetAllTestProcess();
        TestProcessDTO? GetTestProcessById(int id);
        bool CreateTestProcess(TestProcessDTO testProcess);
        bool UpdateTestProcess(TestProcessDTO testProcess);
        bool DeleteTestProcessById(int id);
    }
}
