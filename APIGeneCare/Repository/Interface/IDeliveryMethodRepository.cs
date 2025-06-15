using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IDeliveryMethodRepository
    {
        IEnumerable<DeliveryMethod> GetAllDeliveryMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<DeliveryMethod> GetAllDeliveryMethods();
        DeliveryMethod? GetDeliveryMethodById(int id);
        bool CreateBlog(DeliveryMethod deliveryMethod);
        bool UpdateBlog(DeliveryMethod deliveryMethod);
        bool DeleteDeliveryMethodById(int id);
    }
}
