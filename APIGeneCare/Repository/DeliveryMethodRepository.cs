// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class DeliveryMethodRepository : IDeliveryMethodRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public DeliveryMethodRepository(GeneCareContext context) => _context = context;

        public IEnumerable<DeliveryMethodDTO> GetAllDeliveryMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DeliveryMethodDTO> GetAllDeliveryMethods()
            => _context.DeliveryMethods.Select(dm => new DeliveryMethodDTO
            {
                DeliveryMethodId = dm.DeliveryMethodId,
                DeliveryMethodName = dm.DeliveryMethodName,
            }).ToList();
        public DeliveryMethodDTO? GetDeliveryMethodById(int id)
            => _context.DeliveryMethods.Select(dm => new DeliveryMethodDTO
            {
                DeliveryMethodId = dm.DeliveryMethodId,
                DeliveryMethodName = dm.DeliveryMethodName,
            }).SingleOrDefault(dm => dm.DeliveryMethodId == id);
        public bool CreateDeliveryMethod(DeliveryMethodDTO deliveryMethod)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (deliveryMethod == null ||
                    String.IsNullOrWhiteSpace(deliveryMethod.DeliveryMethodName))
                    return false;

                _context.DeliveryMethods.Add(new DeliveryMethod
                {
                    DeliveryMethodName = deliveryMethod.DeliveryMethodName
                });

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool UpdateDeliveryMethod(DeliveryMethodDTO deliveryMethod)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (deliveryMethod == null ||
                    String.IsNullOrWhiteSpace(deliveryMethod.DeliveryMethodName))
                    return false;

                var existDeliveryMethod = _context.DeliveryMethods.Find(deliveryMethod.DeliveryMethodId);
                if (existDeliveryMethod == null)
                    return false;

                existDeliveryMethod.DeliveryMethodId = deliveryMethod.DeliveryMethodId;
                existDeliveryMethod.DeliveryMethodName = deliveryMethod.DeliveryMethodName;

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
        public bool DeleteDeliveryMethodById(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var deliveryMethod = _context.DeliveryMethods.Find(id);
                if (deliveryMethod == null) return false;

                _context.DeliveryMethods.Remove(deliveryMethod);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
