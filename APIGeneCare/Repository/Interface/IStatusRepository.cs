using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IStatusRepository
    {
        IEnumerable<StatusDTO> GetAllStatusPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<StatusDTO> GetAllStatus();
        StatusDTO? GetStatusById(int id);
        bool CreateStatus(StatusDTO status);
        bool UpdateStatus(StatusDTO status);
        bool DeleteStatusById(int id);
    }
}
