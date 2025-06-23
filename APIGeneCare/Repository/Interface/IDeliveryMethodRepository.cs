// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IDeliveryMethodRepository
    {
        IEnumerable<DeliveryMethodDTO> GetAllDeliveryMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<DeliveryMethodDTO> GetAllDeliveryMethods();
        DeliveryMethodDTO? GetDeliveryMethodById(int id);
        bool CreateDeliveryMethodBy(DeliveryMethodDTO deliveryMethod);
        bool UpdateDeliveryMethodBy(DeliveryMethodDTO deliveryMethod);
        bool DeleteDeliveryMethodById(int id);
    }
}
