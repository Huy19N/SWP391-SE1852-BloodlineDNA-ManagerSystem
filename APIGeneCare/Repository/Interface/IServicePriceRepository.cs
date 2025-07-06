using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IServicePriceRepository
    {
        IEnumerable<ServicePriceDTO> GetAllServicePricesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        ServicePriceDTO? GetServicePriceById(int id);
        bool CreateServicePrice(ServicePriceDTO servicePrice);
        bool UpdateServicePrice(ServicePriceDTO servicePrice);
        bool DeleteServicePriceById(int id);
    }
}
