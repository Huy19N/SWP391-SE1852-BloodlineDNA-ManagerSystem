// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetAllServicesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<Service> GetAllServices();
        Service? GetServiceById(int id);
        bool CreateService(Service service);
        bool UpdateService(Service service);
        bool DeleteServiceById(int id);
    }
}
