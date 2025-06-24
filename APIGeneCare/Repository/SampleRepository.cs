// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class SampleRepository : ISampleRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public SampleRepository(GeneCareContext context) => _context = context;
        public IEnumerable<SampleDTO> GetAllSamplesPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allSamples = _context.Samples.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("sampleid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int sampleid))
                    {
                        allSamples = _context.Samples.Where(s => s.SampleId == sampleid);
                    }
                }

                if (typeSearch.Equals("bookingid", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (int.TryParse(search, out int bookingid))
                    {
                        allSamples = _context.Samples.Where(s => s.BookingId == bookingid);
                    }
                }

                if (typeSearch.Equals("samplevariant", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = _context.Samples.Where(s => !String.IsNullOrWhiteSpace(s.SampleVariant) &&
                    s.SampleVariant.Contains(search, StringComparison.CurrentCultureIgnoreCase));

                if (typeSearch.Equals("collectby", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int collectby))
                        allSamples = _context.Samples.Where(s => s.CollectBy == collectby);
                if (typeSearch.Equals("deliverymethodid", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int deliverymethodid))
                        allSamples = _context.Samples.Where(s => s.DeliveryMethodId == deliverymethodid);
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
            return result.Select(s => new SampleDTO
            {
                SampleId = s.SampleId,
                BookingId = s.BookingId,
                PatientId = s.PatientId,
                Date = s.Date,
                SampleVariant = s.SampleVariant,
                CollectBy = s.CollectBy,
                DeliveryMethodId = s.DeliveryMethodId,
                Status = s.Status,
            });
        }
        public SampleDTO? GetSampleById(int id)
            => _context.Samples.Select(s => new SampleDTO
            {
                SampleId = s.SampleId,
                BookingId = s.BookingId,
                PatientId = s.PatientId,
                Date = s.Date,
                SampleVariant = s.SampleVariant,
                CollectBy = s.CollectBy,
                DeliveryMethodId = s.DeliveryMethodId,
                Status = s.Status,
            }).SingleOrDefault(s => s.SampleId == id);
        public bool CreateSample(SampleDTO sample)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (sample == null)
                {
                    return false;
                }
                _context.Samples.Add(new Sample
                {
                    BookingId = sample.BookingId,
                    PatientId = sample.PatientId,
                    Date = sample.Date,
                    SampleVariant = sample.SampleVariant,
                    CollectBy = sample.CollectBy,
                    DeliveryMethodId = sample.DeliveryMethodId,
                    Status = sample.Status
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
        public bool UpdateSample(SampleDTO sample)
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
                existingSample.PatientId = sample.PatientId;
                existingSample.Date = sample.Date;
                existingSample.SampleVariant = sample.SampleVariant;
                existingSample.CollectBy = sample.CollectBy;
                existingSample.DeliveryMethodId = sample.DeliveryMethodId;
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
        public bool DeleteSampleById(int id)
        {
            var Sample = _context.Samples.Find(id);
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
    }
}
