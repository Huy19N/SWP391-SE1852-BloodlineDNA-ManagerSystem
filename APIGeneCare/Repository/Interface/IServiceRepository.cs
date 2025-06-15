using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetAllServicesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        Service? GetServiceById(int id);
        bool CreateService(Service service);
        bool UpdateService(Service service);
        bool DeleteServiceById(int id);
    }
}
