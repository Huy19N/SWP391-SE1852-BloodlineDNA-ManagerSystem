using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface ITestResultRepository
    {
        IEnumerable<TestResultDTO> GetAllTestResultsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        TestResultDTO? GetTestResultsById(int id);
        bool CreateTestResults(TestResultDTO testResult);
        bool UpdateTestResults(TestResultDTO testResult);
        bool DeleteTestResultsById(int id);
    }
}
