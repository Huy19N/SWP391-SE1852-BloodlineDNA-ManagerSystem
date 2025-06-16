using APIGeneCare.Data;

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
