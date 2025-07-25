﻿using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class ServicePriceRepository : IServicePriceRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 100;
        public ServicePriceRepository(GeneCareContext context) => _context = context;

        public IEnumerable<ServicePriceDTO> GetAllServicePricesPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allServicePrices = _context.ServicePrices.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("priceid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int priceid))
                    {
                        allServicePrices = _context.ServicePrices.Where(sp => sp.PriceId == priceid);
                    }
                }

                if (typeSearch.Equals("serviceid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int serviceid))
                    {
                        allServicePrices = _context.ServicePrices.Where(sp => sp.ServiceId == serviceid);
                    }
                }

                if (typeSearch.Equals("durationid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int durationid))
                    {
                        allServicePrices = _context.ServicePrices.Where(sp => sp.DurationId == durationid);
                    }
                }

                if (typeSearch.Equals("price", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int price))
                    {
                        allServicePrices = _context.ServicePrices.Where(sp => sp.Price == price);
                    }
                }
            }
            #endregion
            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("priceid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allServicePrices = allServicePrices.OrderBy(sp => sp.PriceId);

                if (sortBy.Equals("priceid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allServicePrices = allServicePrices.OrderByDescending(sp => sp.ServiceId);

                if (sortBy.Equals("serviceid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allServicePrices = allServicePrices.OrderBy(sp => sp.ServiceId);

                if (sortBy.Equals("serviceid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allServicePrices = allServicePrices.OrderByDescending(sp => sp.ServiceId);

                if (sortBy.Equals("durationid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allServicePrices = allServicePrices.OrderBy(sp => sp.DurationId);

                if (sortBy.Equals("durationid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allServicePrices = allServicePrices.OrderByDescending(sp => sp.DurationId);

                if (sortBy.Equals("price_asc", StringComparison.CurrentCultureIgnoreCase))
                    allServicePrices = allServicePrices.OrderBy(sp => sp.ServiceId);

                if (sortBy.Equals("price_desc", StringComparison.CurrentCultureIgnoreCase))
                    allServicePrices = allServicePrices.OrderByDescending(sp => sp.ServiceId);

            }
            #endregion
            var result = PaginatedList<ServicePrice>.Create(allServicePrices, page ?? 1, PAGE_SIZE);
            return result.Select(sp => new ServicePriceDTO
            {
                PriceId = sp.PriceId,
                ServiceId = sp.ServiceId,
                DurationId = sp.DurationId,
                Price = sp.Price,
                IsDeleted = sp.IsDeleted,
            });
        }
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
