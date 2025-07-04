using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IDurationRepository
    {
        IEnumerable<DurationDTO> GetAllDurationsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<DurationDTO> GetAllDurations();
        DurationDTO? GetDurationById(int id);
        bool CreateDuration(DurationDTO duration);
        bool UpdateDuration(DurationDTO duration);
        bool DeleteDurationById(int id);
    }
}
