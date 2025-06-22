// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;

namespace APIGeneCare.Repository.Interface
{
    public interface ITestProcessRepository
    {
        IEnumerable<TestProcess> GetAllTestProcessPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<TestProcess> GetAllTestProcess();
        TestProcess? GetTestProcessById(int id);
        bool CreateTestProcess(TestProcess testProcess);
        bool UpdateTestProcess(TestProcess testProcess);
        bool DeleteTestProcessById(int id);
    }
}
