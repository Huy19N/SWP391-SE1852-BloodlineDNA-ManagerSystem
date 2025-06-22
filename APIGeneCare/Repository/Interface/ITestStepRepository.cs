// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;

namespace APIGeneCare.Repository.Interface
{
    public interface ITestStepRepository
    {
        IEnumerable<TestStep> GetAllTestStepsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<TestStep> GetAllTestStep();
        TestStep? GetTestStepById(int id);
        bool CreateTestStep(TestStep testStep);
        bool UpdateTestStep(TestStep testStep);
        bool DeleteTestStepById(int id);
    }
}
