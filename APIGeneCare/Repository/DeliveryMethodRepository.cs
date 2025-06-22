// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace APIGeneCare.Repository
{
    public class DeliveryMethodRepository : IDeliveryMethodRepository
    {
        private readonly GeneCareContext _context;
        public DeliveryMethodRepository (GeneCareContext context) => _context = context;
        public bool CreateDeliveryMethodBy(DeliveryMethod deliveryMethod)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDeliveryMethodById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeliveryMethod> GetAllDeliveryMethods()
            => _context.DeliveryMethods.OrderBy(dm => dm.DeliveryMethodId);

        public IEnumerable<DeliveryMethod> GetAllDeliveryMethodsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public DeliveryMethod? GetDeliveryMethodById(int id)
            => _context.DeliveryMethods.Find(id);

        public bool UpdateDeliveryMethodBy(DeliveryMethod deliveryMethod)
        {
            throw new NotImplementedException();
        }
    }
}
