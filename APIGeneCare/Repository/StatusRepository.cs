using APIGeneCare.Data;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class StatusRepository : IStatusRepository
    {
        public bool CreateStatus(Status status)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStatusById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Status> GetAllStatusPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public Status? GetStatusById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(Status status)
        {
            throw new NotImplementedException();
        }
    }
}
