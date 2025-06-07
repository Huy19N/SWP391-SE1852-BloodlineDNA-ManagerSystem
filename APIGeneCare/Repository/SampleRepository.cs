using APIGeneCare.Data;
using APIGeneCare.Model;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class SampleRepository : ISampleRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public SampleRepository(GeneCareContext context) => _context = context;
        public bool CreateSample(Sample sample)
        {
            if (sample == null)
            {
                return false;
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Samples.Add(sample);
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
        public bool DeleteSample(int id)
        {
            var Sample =_context.Samples.Find(id);
            if (Sample == null) return false;
            
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Samples.Remove(Sample);
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
        public IEnumerable<Sample> GetAllSamples(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allSamples = _context.Samples.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("sampleid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if(int.TryParse(search,out int sampleid))
                    {
                        allSamples = _context.Samples.Where(s => s.SampleId == sampleid);
                    }
                }

                if (typeSearch.Equals("bookingid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if(int.TryParse(search, out int bookingid))
                    {
                        allSamples = _context.Samples.Where(s => s.BookingId == bookingid);
                    }
                }

                if (typeSearch.Equals("samplevariant", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = _context.Samples.Where(s => !String.IsNullOrWhiteSpace(s.SampleVariant) &&
                    s.SampleVariant.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                if (typeSearch.Equals("collectby", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = _context.Samples.Where(s => !String.IsNullOrWhiteSpace(s.CollectBy) &&
                    s.CollectBy.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                if (typeSearch.Equals("deliverymethod", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = _context.Samples.Where(s => !String.IsNullOrWhiteSpace(s.DeliveryMethod) &&
                    s.DeliveryMethod.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                if (typeSearch.Equals("status", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = _context.Samples.Where(s => !String.IsNullOrWhiteSpace(s.Status) &&
                    s.Status.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            }
            #endregion
            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("sampleid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderBy(s => s.SampleId);

                if (sortBy.Equals("sampleid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderByDescending(s => s.SampleId);

                if (sortBy.Equals("bookingid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderBy(s => s.BookingId);

                if (sortBy.Equals("bookingid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderByDescending(s => s.BookingId);

                if (sortBy.Equals("samplevariant_asc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderBy(s => s.SampleVariant);

                if (sortBy.Equals("samplevariant_desc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderByDescending(s => s.SampleVariant);

                if (sortBy.Equals("deliverymethod_asc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderBy(s => s.DeliveryMethod);

                if (sortBy.Equals("deliverymethod_desc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderByDescending(s => s.DeliveryMethod);

                if (sortBy.Equals("status_asc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderBy(s => s.Status);

                if (sortBy.Equals("status_desc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderByDescending(s => s.Status);
            }
            #endregion
            var result = PaginatedList<Sample>.Create(allSamples, page ?? 1, PAGE_SIZE);
            return result.Select(s => new Sample
            {
                SampleId = s.SampleId,
                BookingId = s.BookingId,
                Date = s.Date,
                SampleVariant = s.SampleVariant,
                CollectBy = s.CollectBy,
                DeliveryMethod = s.DeliveryMethod,
                Status = s.Status,
            });
        }

        public Sample? GetSampleById(int id)
            => _context.Samples.Find(id);

        public bool UpdateSample(Sample sample)
        {
            if (sample == null)
            {
                return false;
            }

            var existingSample = _context.Samples.Find(sample.SampleId);
            if (existingSample == null)
            {
                return false;
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existingSample.BookingId = sample.BookingId;
                existingSample.Date = sample.Date;
                existingSample.SampleVariant = sample.SampleVariant;
                existingSample.CollectBy = sample.CollectBy;
                existingSample.DeliveryMethod = sample.DeliveryMethod;
                existingSample.Status = sample.Status;

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
