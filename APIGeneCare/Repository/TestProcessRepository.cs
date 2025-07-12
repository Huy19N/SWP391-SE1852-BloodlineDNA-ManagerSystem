using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class TestProcessRepository : ITestProcessRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public TestProcessRepository(GeneCareContext context) => _context = context;

        public IEnumerable<TestProcessDTO> GetAllTestProcessPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allTestProcess = _context.TestProcesses.AsQueryable();
            #region search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("processid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int processId))
                        allTestProcess = allTestProcess.Where(tp => tp.ProcessId == processId);
                }
                else if (typeSearch.Equals("bookingid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int bookingId))
                        allTestProcess = allTestProcess.Where(tp => tp.BookingId == bookingId);
                }
                else if (typeSearch.Equals("stepid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int stepId))
                        allTestProcess = allTestProcess.Where(tp => tp.StepId == stepId);
                }
                else if (typeSearch.Equals("statusid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int statusId))
                        allTestProcess = allTestProcess.Where(tp => tp.StatusId == statusId);
                }
                else if (typeSearch.Equals("description", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestProcess = allTestProcess.Where(tp => !String.IsNullOrWhiteSpace(tp.Description) &&
                        tp.Description.Contains(search, StringComparison.CurrentCultureIgnoreCase));
                }
                else if (typeSearch.Equals("updateat", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestProcess = allTestProcess.Where(tp => tp.UpdatedAt != null && tp.UpdatedAt.Value.ToString("yyyy-MM-dd").Contains(search));
                }
            }
            #endregion
            #region sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.Trim().ToLower())
                {
                    case "processid_asc":
                        allTestProcess = allTestProcess.OrderBy(tp => tp.ProcessId);
                        break;
                    case "processid_desc":
                        allTestProcess = allTestProcess.OrderByDescending(tp => tp.ProcessId);
                        break;
                    case "bookingid_asc":
                        allTestProcess = allTestProcess.OrderBy(tp => tp.ProcessId);
                        break;
                    case "bookingid_desc":
                        allTestProcess = allTestProcess.OrderByDescending(tp => tp.BookingId);
                        break;
                    case "stepid_asc":
                        allTestProcess = allTestProcess.OrderBy(tp => tp.StepId);
                        break;
                    case "stepid_desc":
                        allTestProcess = allTestProcess.OrderByDescending(tp => tp.StepId);
                        break;
                    case "statusid_asc":
                        allTestProcess = allTestProcess.OrderBy(tp => tp.StatusId);
                        break;
                    case "statusid_desc":
                        allTestProcess = allTestProcess.OrderByDescending(tp => tp.StatusId);
                        break;
                    case "description_asc":
                        allTestProcess = allTestProcess.OrderBy(tp => tp.Description);
                        break;
                    case "description_desc":
                        allTestProcess = allTestProcess.OrderByDescending(tp => tp.Description);
                        break;
                    case "updateat_asc":
                        allTestProcess = allTestProcess.OrderBy(tp => tp.UpdatedAt);
                        break;
                    case "updateat_desc":
                        allTestProcess = allTestProcess.OrderByDescending(tp => tp.UpdatedAt);
                        break;
                    default:

                        break;
                }
            }
            #endregion

            var result = PaginatedList<TestProcess>.Create(allTestProcess, page ?? 1, PAGE_SIZE);
            return result.Select(tp => new TestProcessDTO
            {
                ProcessId = tp.ProcessId,
                BookingId = tp.BookingId,
                StepId = tp.StepId,
                StatusId = tp.StatusId,
                Description = tp.Description,
                UpdatedAt = tp.UpdatedAt,
            });
        }
        public IEnumerable<TestProcessDTO> GetAllTestProcess()
            => _context.TestProcesses.Select(tp => new TestProcessDTO
            {
                ProcessId = tp.ProcessId,
                BookingId = tp.BookingId,
                StepId = tp.StepId,
                StatusId = tp.StatusId,
                Description = tp.Description,
                UpdatedAt = tp.UpdatedAt,
            }).ToList();
        public TestProcessDTO? GetTestProcessById(int id)
            => _context.TestProcesses.Select(tp => new TestProcessDTO
            {
                ProcessId = tp.ProcessId,
                BookingId = tp.BookingId,
                StepId = tp.StepId,
                StatusId = tp.StatusId,
                Description = tp.Description,
                UpdatedAt = tp.UpdatedAt
            }).SingleOrDefault(tp => tp.ProcessId == id);
        public bool CreateTestProcess(TestProcessDTO testProcess)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (testProcess == null) return false;
                if (_context.TestProcesses.Find(testProcess.ProcessId) != null) return false;
                _context.TestProcesses.Add(new TestProcess
                {
                    BookingId = testProcess.BookingId,
                    StepId = testProcess.StepId,
                    StatusId = testProcess.StatusId,
                    Description = testProcess.Description,
                    UpdatedAt = testProcess.UpdatedAt,
                });

                _context.SaveChanges();
                transaction.Commit();
                return true;

            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public bool UpdateTestProcess(TestProcessDTO testProcess)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (testProcess == null) return false;
                var existTestProcess = _context.TestProcesses.FirstOrDefault(x=> x.ProcessId == testProcess.ProcessId);
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
                throw;
            }
        }
        public bool DeleteTestProcessById(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var testProcess = _context.TestProcesses.Find(id);
                if (testProcess == null) return false;

                _context.TestProcesses.Remove(testProcess);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
