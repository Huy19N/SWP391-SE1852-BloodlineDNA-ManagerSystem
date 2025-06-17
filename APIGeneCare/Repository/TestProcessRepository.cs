using APIGeneCare.Data;
using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class TestProcessRepository : ITestProcessRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public TestProcessRepository(GeneCareContext context) => _context = context;
        
        public IEnumerable<TestProcess> GetAllTestProcessPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allTestProcess = _context.TestProcesses.AsQueryable();

            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search)) 
            {
                
            }

            if (!String.IsNullOrWhiteSpace(sortBy))
            {

            }
            var result = PaginatedList<TestProcess>.Create(allTestProcess,page ?? 1,PAGE_SIZE);
            return result.Select(tp => new TestProcess
            {
                ProcessId = tp.ProcessId,
                BookingId = tp.BookingId,
                StepId = tp.StepId,
                StatusId = tp.StatusId,
                Description = tp.Description,
                UpdatedAt   = tp.UpdatedAt,
            });
        }
        public IEnumerable<TestProcess> GetAllTestProcess()
        {
            throw new NotImplementedException();
        }
        
        public TestProcess? GetTestProcessById(int id)
            => _context.TestProcesses.Find(id);
        public bool CreateTestProcess(TestProcess testProcess)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if(testProcess == null) return false;
                if(_context.TestProcesses.Find(testProcess.ProcessId) != null) return false;
                _context.TestProcesses.Add(testProcess);

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
        public bool DeleteTestProcessById(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var testProcess = GetTestProcessById(id);
                if ( testProcess == null) return false;
                
                _context.TestProcesses.Remove(testProcess);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }

 _context.TestProcesses.Remove(GetTestProcessById(id));
        }
        public bool UpdateTestProcess(TestProcess testProcess)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (testProcess == null) return false;
                var existTestProcess = GetTestProcessById(testProcess.ProcessId);
                if (existTestProcess == null) return false;

                existTestProcess.BookingId = testProcess.BookingId;
                existTestProcess.StepId = testProcess.StepId;  
                existTestProcess.StatusId = testProcess.StatusId;
                existTestProcess.Description = testProcess.Description;
                existTestProcess.UpdatedAt = testProcess.UpdatedAt;

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
