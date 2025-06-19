// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IStatusRepository
    {
        IEnumerable<Status> GetAllStatusPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<Status> GetAllStatus();
        Status? GetStatusById(int id);
        bool CreateStatus(Status status);
        bool UpdateStatus(Status status);
        bool DeleteStatusById(int id);
    }
}
