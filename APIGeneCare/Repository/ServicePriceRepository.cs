using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;

namespace APIGeneCare.Repository
{
    public class ServicePriceRepository : IServicePriceRepository
    {
        private readonly GeneCareContext _context;
        public ServicePriceRepository(GeneCareContext context) => _context = context;

        public async Task<PagingModel> GetAllServicePricesPaging(string? search, int page, int itemPerPage)
        {
            var query = _context.ServicePrices.Where(x => x.ServiceId.ToString().Contains(search??"") ||
                                                         x.Price.ToString().Contains(search??"") || 
                                                         x.DurationId.ToString().Contains(search??"") ||
                                                         x.PriceId.ToString().Contains(search??""));

            int totalRecords = await query.CountAsync();

            int maxPage = (int)Math.Ceiling((double)totalRecords / itemPerPage);

            var pagedData = await query.Skip((page - 1) * itemPerPage)
                                       .Take(itemPerPage)
                                       .Select(sp => new ServicePriceDTO{
                                           PriceId = sp.PriceId,
                                           ServiceId = sp.ServiceId,
                                           DurationId = sp.DurationId,
                                           Price = sp.Price,
                                           IsDeleted = sp.IsDeleted
                                       }).ToListAsync();

            return new PagingModel() {
                MaxPage = maxPage,
                CurrentPage = page,
                Data = pagedData
            };
        }
        public async Task<IEnumerable<ServicePriceDTO>> GetAllServicePrices()
            => await _context.ServicePrices.Select(x => new ServicePriceDTO
            {
                PriceId = x.PriceId,
                ServiceId = x.ServiceId,
                DurationId = x.DurationId,
                Price = x.Price,
                IsDeleted = x.IsDeleted
            }).ToListAsync();
        public ServicePriceDTO? GetServicePriceById(int id)
            => _context.ServicePrices.Select(sp => new ServicePriceDTO
            {
                PriceId = sp.PriceId,
                ServiceId = sp.ServiceId,
                DurationId = sp.DurationId,
                Price = sp.Price,
                IsDeleted = sp.IsDeleted,
            }).SingleOrDefault(sp => sp.PriceId == id);
        public bool CreateServicePrice(ServicePriceDTO servicePrice)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (servicePrice == null) return false;
                var existingServicePrice = _context.ServicePrices
                    .FirstOrDefault(sp => sp.ServiceId == servicePrice.ServiceId && sp.DurationId == servicePrice.DurationId && !sp.IsDeleted);
                if (existingServicePrice != null)
                {
                    existingServicePrice.IsDeleted = true;
                    _context.ServicePrices.Update(existingServicePrice);
                    _context.SaveChanges();
                }

                _context.ServicePrices.Add(new ServicePrice
                {
                    ServiceId = servicePrice.ServiceId,
                    DurationId = servicePrice.DurationId,
                    Price = servicePrice.Price
                });
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public bool UpdateServicePrice(ServicePriceDTO servicePrice)
        {
            var existingServicePrice = _context.ServicePrices.Find(servicePrice.PriceId);
            if (servicePrice == null)
            {
                return false;
            }
            if (existingServicePrice == null)
            {
                return false;
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existingServicePrice.IsDeleted = true;
                _context.ServicePrices.Add(new ServicePrice
                {
                    ServiceId = servicePrice.ServiceId,
                    DurationId = servicePrice.DurationId,
                    Price = servicePrice.Price
                });
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public bool DeleteServicePriceById(int id)
        {
            var servicePrice = _context.ServicePrices.Find(id);
            if (servicePrice == null) return false;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                servicePrice.IsDeleted = true;

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
