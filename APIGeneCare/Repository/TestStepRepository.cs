using APIGeneCare.Data;
using APIGeneCare.Repository.Interface;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace APIGeneCare.Repository
{
    public class TestStepRepository : ITestStepRepository
    {
        private readonly GeneCareContext _context;
        public TestStepRepository(GeneCareContext context) => _context = context;
        public IEnumerable<TestStep> GetAllTestStepsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TestStep> GetAllTestStep()
            => _context.TestSteps.ToList();
        
        public TestStep? GetTestStepById(int id)
            => _context.TestSteps.Find(id);
        public bool UpdateTestStep(TestStep testStep)
        {
            if(testStep == null || String.IsNullOrWhiteSpace(testStep.StepName) )return false;
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
        public bool CreateTestStep(TestStep testStep)
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
