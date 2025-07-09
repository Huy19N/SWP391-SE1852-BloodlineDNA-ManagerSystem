using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public StatusRepository(GeneCareContext context) => _context = context;
        public IEnumerable<StatusDTO> GetAllStatusPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<StatusDTO> GetAllStatus()
            => _context.Statuses.Select(s => new StatusDTO
            {
                StatusId = s.StatusId,
                StatusName = s.StatusName
            }).ToList();

        public StatusDTO? GetStatusById(int id)
            => _context.Statuses.Select(s => new StatusDTO
            {
                StatusId = s.StatusId,
                StatusName = s.StatusName
            }).SingleOrDefault(s => s.StatusId == id);
        public bool CreateStatus(StatusDTO status)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (status == null || GetStatusById(status.StatusId) != null) return false;
                _context.Statuses.Add(new Status
                {
                    StatusName = status.StatusName,
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
        public bool UpdateStatus(StatusDTO status)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var existStatus = _context.Statuses.Find(status.StatusId);
                if (existStatus == null) return false;
                existStatus.StatusName = status.StatusName;

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
        public bool DeleteStatusById(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var status = _context.Statuses.Find(id);
                if (status == null) return false;
                _context.Statuses.Remove(status);

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
