using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IServiceRepository
    {
        IEnumerable<ServiceDTO> GetAllServicesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<ServiceDTO> GetAllServices();
        ServiceDTO? GetServiceById(int id);
        bool CreateService(ServiceDTO service);
        bool UpdateService(ServiceDTO service);
        bool DeleteServiceById(int id);
    }
}
