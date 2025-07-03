// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 20;
        public ServiceRepository(GeneCareContext context) => _context = context;
        public IEnumerable<ServiceDTO> GetAllServicesPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allServices = _context.Services.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("serviceid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int serviceid))
                    {
                        allServices = _context.Services.Where(s => s.ServiceId == serviceid);
                    }
                }

                if (typeSearch.Equals("servicename", StringComparison.CurrentCultureIgnoreCase))
                    allServices = _context.Services.Where(s => !String.IsNullOrWhiteSpace(s.ServiceName) &&
                    s.ServiceName.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                if (typeSearch.Equals("servicetype", StringComparison.CurrentCultureIgnoreCase))
                    allServices = _context.Services.Where(s => !String.IsNullOrWhiteSpace(s.ServiceType) &&
                    s.ServiceType.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                if (typeSearch.Equals("description", StringComparison.CurrentCultureIgnoreCase))
                    allServices = _context.Services.Where(s => !String.IsNullOrWhiteSpace(s.Description) &&
                    s.Description.Contains(search, StringComparison.CurrentCultureIgnoreCase));

            }
            #endregion
            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("serviceid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allServices = allServices.OrderBy(s => s.ServiceId);

                if (sortBy.Equals("serviceid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allServices = allServices.OrderByDescending(s => s.ServiceId);

                if (sortBy.Equals("servicename_asc", StringComparison.CurrentCultureIgnoreCase))
                    allServices = allServices.OrderBy(s => s.ServiceName);

                if (sortBy.Equals("servicename_desc", StringComparison.CurrentCultureIgnoreCase))
                    allServices = allServices.OrderByDescending(s => s.ServiceName);

                if (sortBy.Equals("servicetype_asc", StringComparison.CurrentCultureIgnoreCase))
                    allServices = allServices.OrderBy(s => s.ServiceType);

                if (sortBy.Equals("servicetype_desc", StringComparison.CurrentCultureIgnoreCase))
                    allServices = allServices.OrderByDescending(s => s.ServiceType);

                if (sortBy.Equals("description_asc", StringComparison.CurrentCultureIgnoreCase))
                    allServices = allServices.OrderBy(s => s.Description);

                if (sortBy.Equals("description_desc", StringComparison.CurrentCultureIgnoreCase))
                    allServices = allServices.OrderByDescending(s => s.Description);

            }
            #endregion

            var result = PaginatedList<Service>.Create(allServices, page ?? 1, PAGE_SIZE);
            return result.Select(s => new ServiceDTO
            {
                ServiceId = s.ServiceId,
                ServiceName = s.ServiceName,
                ServiceType = s.ServiceType,
                Description = s.Description
            });
        }
        public IEnumerable<ServiceDTO> GetAllServices()
            => _context.Services.Select(s => new ServiceDTO
            {
                ServiceId = s.ServiceId,
                ServiceName = s.ServiceName,
                ServiceType = s.ServiceType,
                Description = s.Description
            }).ToList();
        public ServiceDTO? GetServiceById(int id)
            => _context.Services.Select(s => new ServiceDTO
            {
                ServiceId = s.ServiceId,
                ServiceName = s.ServiceName,
                ServiceType = s.ServiceType,
                Description = s.Description
            }).SingleOrDefault(s => s.ServiceId == id);
        public bool CreateService(ServiceDTO service)
        {
            if (service == null)
            {
                return false;
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Services.Add(new Service
                {
                    ServiceName = service.ServiceName,
                    ServiceType = service.ServiceType,
                    Description = service.Description
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
        public bool UpdateService(ServiceDTO service)
        {
            if (service == null)
            {
                return false;
            }
            var existingService = _context.Services.Find(service.ServiceId);
            if (existingService == null)
            {
                return false;
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existingService.ServiceName = service.ServiceName;
                existingService.ServiceType = service.ServiceType;
                existingService.Description = service.Description;

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
        public bool DeleteServiceById(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null) return false;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Services.Remove(service);
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
