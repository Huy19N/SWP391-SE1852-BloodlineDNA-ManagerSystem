using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class DurationRepository : IDurationRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public DurationRepository(GeneCareContext context) => _context = context;

        public IEnumerable<DurationDTO> GetAllDurationsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allDurations = _context.Durations.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("durationid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int durationid))
                        allDurations = _context.Durations.Where(d => d.DurationId == durationid);

                }

                if (typeSearch.Equals("durationname", StringComparison.CurrentCultureIgnoreCase))
                    allDurations = _context.Durations.Where(d => !String.IsNullOrWhiteSpace(d.DurationName) &&
                    d.DurationName.Equals(search, StringComparison.CurrentCultureIgnoreCase));

            }
            #endregion
            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("durationid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allDurations = allDurations.OrderBy(d => d.DurationId);

                if (sortBy.Equals("durationid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allDurations = allDurations.OrderByDescending(d => d.DurationId);

                if (sortBy.Equals("durationname_asc", StringComparison.CurrentCultureIgnoreCase))
                    allDurations = allDurations.OrderBy(d => d.DurationName);

                if (sortBy.Equals("durationname_desc", StringComparison.CurrentCultureIgnoreCase))
                    allDurations = allDurations.OrderByDescending(d => d.DurationName);

                if (sortBy.Equals("time_asc", StringComparison.CurrentCultureIgnoreCase))
                    allDurations = allDurations.OrderBy(d => d.Time);

                if (sortBy.Equals("time_desc", StringComparison.CurrentCultureIgnoreCase))
                    allDurations = allDurations.OrderByDescending(d => d.Time);

            }
            #endregion

            var result = PaginatedList<Duration>.Create(allDurations, page ?? 1, PAGE_SIZE);
            return result.Select(d => new DurationDTO
            {
                DurationId = d.DurationId,
                DurationName = d.DurationName,
                Time = d.Time,
            });

        }
        public IEnumerable<DurationDTO> GetAllDurations()
            => _context.Durations.Select(d => new DurationDTO
            {
                DurationId = d.DurationId,
                DurationName = d.DurationName,
                Time = d.Time,
            }).ToList();
        public DurationDTO? GetDurationById(int id)
            => _context.Durations.Select(d => new DurationDTO
            {
                DurationId = d.DurationId,
                DurationName = d.DurationName,
                Time = d.Time,
            }).SingleOrDefault(d => d.DurationId == id);
        public bool CreateDuration(DurationDTO duration)
        {
            
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (duration == null || String.IsNullOrWhiteSpace(duration.DurationName))
                {
                    return false;
                }
                _context.Durations.Add(new Duration
                {
                    DurationName = duration.DurationName,
                    Time = duration.Time,
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
        public bool UpdateDuration(DurationDTO duration)
        {
            
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (duration == null ||
                    String.IsNullOrWhiteSpace(duration.DurationName))
                {
                    return false;
                }
                var existingDuration = _context.Durations.Find(duration.DurationId);
                if (existingDuration == null)
                {
                    return false;
                }

                existingDuration.DurationName = duration.DurationName;
                existingDuration.Time = duration.Time;

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
        public bool DeleteDurationById(int id)
        {
            
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var duration = _context.Durations.Find(id);
                if (duration == null) return false;
                _context.Durations.Remove(duration);

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
