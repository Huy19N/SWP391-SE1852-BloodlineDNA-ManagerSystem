// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using APIGeneCare.Entities;

namespace APIGeneCare.Repository.Interface
{
    public interface ITestResultRepository
    {
        IEnumerable<TestResult> GetAllTestResultsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        TestResult? GetTestResultsById(int id);
        bool CreateTestResults(TestResult testResult);
        bool UpdateTestResults(TestResult testResult);
        bool DeleteTestResultsById(int id);
    }
}
