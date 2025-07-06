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
            }
            #endregion
            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("sampleid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderBy(s => s.SampleId);

                if (sortBy.Equals("sampleid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allSamples = allSamples.OrderByDescending(s => s.SampleId);



            }
            #endregion
            var result = PaginatedList<Sample>.Create(allSamples, page ?? 1, PAGE_SIZE);
            return result.Select(s => new SampleDTO
            {
                SampleId = s.SampleId,
                SampleName = s.SampleName
            });

        }
        public SampleDTO? GetSampleById(int id)
            => _context.Samples.Select(s => new SampleDTO
            {
                SampleId = s.SampleId,
                SampleName = s.SampleName
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
                    SampleName = sample.SampleName
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

                existingSample.SampleName = sample.SampleName;

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
