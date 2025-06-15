using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IDurationRepository
    {
        IEnumerable<Duration> GetAllDurationsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        Duration? GetDurationById(int id);
        bool CreateDuration(Duration duration);
        bool UpdateDuration(Duration duration);
        bool DeleteDurationById(int id);
    }
}
