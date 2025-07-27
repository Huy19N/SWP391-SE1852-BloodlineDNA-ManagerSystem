using APIGeneCare.Model;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IServicePriceRepository
    {
        Task<PagingModel> GetAllServicePricesPaging(string? search, int page, int itemPerPage);
        Task<IEnumerable<ServicePriceDTO>> GetAllServicePrices();
        ServicePriceDTO? GetServicePriceById(int id);
        bool CreateServicePrice(ServicePriceDTO servicePrice);
        bool UpdateServicePrice(ServicePriceDTO servicePrice);
        bool DeleteServicePriceById(int id);
    }
}
