// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IDeliveryMethodRepository
    {
        IEnumerable<DeliveryMethod> GetAllDeliveryMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<DeliveryMethod> GetAllDeliveryMethods();
        DeliveryMethod? GetDeliveryMethodById(int id);
        bool CreateDeliveryMethodBy(DeliveryMethod deliveryMethod);
        bool UpdateDeliveryMethodBy(DeliveryMethod deliveryMethod);
        bool DeleteDeliveryMethodById(int id);
    }
}
