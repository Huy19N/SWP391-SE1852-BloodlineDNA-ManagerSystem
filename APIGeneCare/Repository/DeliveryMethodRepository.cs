using APIGeneCare.Data;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class DeliveryMethodRepository : IDeliveryMethodRepository
    {
        public bool CreateBlog(DeliveryMethod deliveryMethod)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDeliveryMethodById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveryMethod> GetAllDeliveryMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public DeliveryMethod? GetDeliveryMethodById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBlog(DeliveryMethod deliveryMethod)
        {
            throw new NotImplementedException();
        }
    }
}
