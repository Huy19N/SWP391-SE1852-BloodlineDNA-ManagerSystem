// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class TestResultRepository : ITestResultRepository
    {

        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public TestResultRepository(GeneCareContext context) => _context = context;
        public IEnumerable<TestResultDTO> GetAllTestResultsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allTestResults = _context.TestResults.AsQueryable();
            #region Search by Type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Trim().Equals("resultid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int resultid))
                    {
                        allTestResults = _context.TestResults.Where(tr => tr.ResultId == resultid);
                    }
                }
                if (typeSearch.Trim().Equals("bookingid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int bookingid))
                    {
                        allTestResults = _context.TestResults.Where(tr => tr.ResultId == bookingid);
                    }
                }
                if (typeSearch.Trim().Equals("resultsummary", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestResults = _context.TestResults.Where(tr => !String.IsNullOrWhiteSpace(tr.ResultSummary) && tr.ResultSummary.Contains(search, StringComparison.CurrentCultureIgnoreCase));
                }
            }
            #endregion
            #region sortby
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Trim().Equals("resultid_asc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestResults = allTestResults.OrderBy(tr => tr.ResultId);
                }

                if (sortBy.Trim().Equals("resultid_desc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestResults = allTestResults.OrderByDescending(tr => tr.ResultId);
                }

                if (sortBy.Trim().Equals("bookingid_asc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestResults = allTestResults.OrderBy(tr => tr.BookingId);
                }

                if (sortBy.Trim().Equals("bookingid_desc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestResults = allTestResults.OrderByDescending(tr => tr.BookingId);
                }

                if (sortBy.Trim().Equals("date_asc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestResults = allTestResults.OrderBy(tr => tr.Date);
                }

                if (sortBy.Trim().Equals("date_desc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestResults = allTestResults.OrderByDescending(tr => tr.Date);
                }

                if (sortBy.Trim().Equals("resultsummary_asc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestResults = allTestResults.OrderBy(tr => tr.ResultSummary);
                }

                if (sortBy.Trim().Equals("resultsummary_desc", StringComparison.CurrentCultureIgnoreCase))
                {
                    allTestResults = allTestResults.OrderByDescending(tr => tr.ResultSummary);
                }

            }
            #endregion

            var result = PaginatedList<TestResult>.Create(allTestResults, page ?? 1, PAGE_SIZE);

            return result.Select(tr => new TestResultDTO
            {
                ResultId = tr.ResultId,
                BookingId = tr.BookingId,
                Date = tr.Date,
                ResultSummary = tr.ResultSummary,
            });
        }
        public TestResultDTO? GetTestResultsById(int id)
            => _context.TestResults.Select(tr => new TestResultDTO
            {
                ResultId = tr.ResultId,
                BookingId = tr.BookingId,
                Date = tr.Date,
                ResultSummary = tr.ResultSummary,
            }).SingleOrDefault(tr => tr.ResultId == id);
        public bool CreateTestResults(TestResultDTO testResult)
        {
            

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (testResult == null)
                {
                    return false;
                }

                _context.TestResults.Add(new TestResult
                {
                    BookingId = testResult.BookingId,
                    Date = testResult.Date,
                    ResultSummary = testResult.ResultSummary,
                });
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
        public bool UpdateTestResults(TestResultDTO testResult)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (testResult == null)
                {
                    return false;
                }
                var existingTestResult = _context.TestResults.Find(testResult.ResultId);
                if (existingTestResult == null)
                {
                    return false;
                }
                existingTestResult.BookingId = testResult.BookingId;
                existingTestResult.Date = testResult.Date;
                existingTestResult.ResultSummary = testResult.ResultSummary;

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
        public bool DeleteTestResultsById(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var testResult = _context.TestResults.Find(id);
                if (testResult == null)
                {
                    return false;
                }
                _context.TestResults.Remove(testResult);
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
