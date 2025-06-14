using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface ITestResultRepository
    {
        IEnumerable<TestResult> GetAllTestResults(string? typeSearch, string? search, string? sortBy, int? page);
        TestResult? GetTestResultsById(int id);
        bool CreateTestResults(TestResult testResult);
        bool UpdateTestResults(TestResult testResult);
        bool DeleteTestResults(int id);
    }
}
