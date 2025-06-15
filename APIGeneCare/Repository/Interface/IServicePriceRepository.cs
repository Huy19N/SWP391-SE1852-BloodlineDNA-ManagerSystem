using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IServicePriceRepository
    {
        IEnumerable<ServicePrice> GetAllServicePricesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        ServicePrice? GetServicePriceById(int id);
        bool CreateServicePrice(ServicePrice servicePrice);
        bool UpdateServicePrice(ServicePrice servicePrice);
        bool DeleteServicePriceById(int id);
    }
}
