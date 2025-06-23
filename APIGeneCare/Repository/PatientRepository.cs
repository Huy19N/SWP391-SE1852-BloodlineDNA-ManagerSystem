using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public PatientRepository(GeneCareContext context) => _context = context;

        public IEnumerable<PatientDTO> GetAllPatientsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<PatientDTO> GetAllPatients()
            => _context.Patients.Select(p => new PatientDTO
            {
                PatientId = p.PatientId,
                BookingId = p.BookingId,
                FullName = p.FullName,
                BirthDate = p.BirthDate,
                Gender = p.Gender,
                IdentifyId = p.IdentifyId,
                SampleType = p.SampleType,
                HasTestedDna = p.HasTestedDna,
                Relationship = p.Relationship
            }).ToList();
        public PatientDTO? GetPatientById(int id)
            => _context.Patients.Select(p => new PatientDTO
            {
                PatientId = p.PatientId,
                BookingId = p.BookingId,
                FullName = p.FullName,
                BirthDate = p.BirthDate,
                Gender = p.Gender,
                IdentifyId = p.IdentifyId,
                SampleType = p.SampleType,
                HasTestedDna = p.HasTestedDna,
                Relationship = p.Relationship
            }).SingleOrDefault(p => p.PatientId == id);
        public bool CreatePatient(PatientDTO patient)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (patient == null)
                {
                    return false;
                }
                _context.Patients.Add(new Patient
                {
                    BookingId = patient.BookingId,
                    FullName = patient.FullName,
                    BirthDate = patient.BirthDate,
                    Gender = patient.Gender,
                    IdentifyId = patient.IdentifyId,
                    SampleType = patient.SampleType,
                    HasTestedDna = patient.HasTestedDna,
                    Relationship = patient.Relationship
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
        public bool UpdatePatient(PatientDTO patient)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (patient == null)
                {
                    return false;
                }

                var existingPatient = _context.Patients.Find(patient.PatientId);
                if (existingPatient == null)
                {
                    return false;
                }
                existingPatient.BookingId = patient.BookingId;
                existingPatient.FullName = patient.FullName;
                existingPatient.BirthDate = patient.BirthDate;
                existingPatient.Gender = patient.Gender;
                existingPatient.IdentifyId = patient.IdentifyId;
                existingPatient.SampleType = patient.SampleType;
                existingPatient.HasTestedDna = patient.HasTestedDna;
                existingPatient.Relationship = patient.Relationship;

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
        public bool DeletePatientById(int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var patient = _context.Patients.Find(id);
                if (patient == null) return false;
                _context.Patients.Remove(patient);

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
