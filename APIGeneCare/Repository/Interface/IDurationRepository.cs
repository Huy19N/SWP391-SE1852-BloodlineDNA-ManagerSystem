// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IDurationRepository
    {
        IEnumerable<Duration> GetAllDurationsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<Duration> GetAllDurations();
        Duration? GetDurationById(int id);
        bool CreateDuration(Duration duration);
        bool UpdateDuration(Duration duration);
        bool DeleteDurationById(int id);
    }
}
