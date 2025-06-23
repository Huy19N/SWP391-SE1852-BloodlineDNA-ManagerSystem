// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class TestStepRepository : ITestStepRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public TestStepRepository(GeneCareContext context) => _context = context;
        public IEnumerable<TestStepDTO> GetAllTestStepsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TestStepDTO> GetAllTestStep()
            => _context.TestSteps.Select(ts => new TestStepDTO
            {
                StepId = ts.StepId,
                StepName = ts.StepName
            }).OrderBy(ts => ts.StepId).ToList();

        public TestStepDTO? GetTestStepById(int id)
            => _context.TestSteps.Find(id);
        public bool UpdateTestStep(TestStepDTO testStep)
        {
            if (testStep == null || String.IsNullOrWhiteSpace(testStep.StepName)) return false;
            var existTestStep = _context.TestSteps.Find(testStep.StepId);
            if (existTestStep == null) return false;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existTestStep.StepName = testStep.StepName;
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }


        }
        public bool CreateTestStep(TestStepDTO testStep)
        {
            if (testStep == null) return false;
            if (_context.TestSteps.Find(testStep.StepId) != null) return false;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.TestSteps.Add(testStep);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool DeleteTestStepById(int id)
        {
            var testStep = GetTestStepById(id);
            if (testStep == null) return false;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.TestSteps.Remove(testStep);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
