// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly GeneCareContext _context;
        public StatusRepository(GeneCareContext context) => _context = context;
        public IEnumerable<Status> GetAllStatus()
            => _context.Statuses.ToList();
        public IEnumerable<Status> GetAllStatusPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }
        public Status? GetStatusById(int id)
            => _context.Statuses.Find(id);
        public bool CreateStatus(Status status)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (status == null || GetStatusById(status.StatusId) != null) return false;
                _context.Statuses.Add(status);

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

        public bool DeleteStatusById(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var status = GetStatusById(id);
                if (status == null) return false;
                _context.Statuses.Remove(status);

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
        public bool UpdateStatus(Status status)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var existStatus = GetStatusById(status.StatusId);
                if (existStatus == null) return false;
                existStatus.StatusName = status.StatusName;

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
