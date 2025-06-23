// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface ITestStepRepository
    {
        IEnumerable<TestStepDTO> GetAllTestStepsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<TestStepDTO> GetAllTestStep();
        TestStepDTO? GetTestStepById(int id);
        bool CreateTestStep(TestStepDTO testStep);
        bool UpdateTestStep(TestStepDTO testStep);
        bool DeleteTestStepById(int id);
    }
}
