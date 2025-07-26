using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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
                SampleId = p.SampleId,
                FullName = p.FullName,
                BirthDate = p.BirthDate,
                Gender = p.Gender,
                IdentifyId = p.IdentifyId,
                HasTestedDna = p.HasTestedDna,
                Relationship = p.Relationship
            });
        public PatientDTO? GetPatientById(int id)
            => _context.Patients.Select(p => new PatientDTO
            {
                PatientId = p.PatientId,
                BookingId = p.BookingId,
                SampleId = p.SampleId,
                FullName = p.FullName,
                BirthDate = p.BirthDate,
                Gender = p.Gender,
                IdentifyId = p.IdentifyId,
                HasTestedDna = p.HasTestedDna,
                Relationship = p.Relationship
            }).SingleOrDefault(p => p.PatientId == id);
        public async Task<int> CreatePatientWithBookingAsync(BookingWithPatient bookingWithPatient)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (bookingWithPatient == null || bookingWithPatient.patients == null || !bookingWithPatient.patients.Any()) return 0;
                var price = await _context.ServicePrices.FirstOrDefaultAsync(sp => sp.PriceId == bookingWithPatient.PriceId && !sp.IsDeleted);
                if (price == null)
                {
                    throw new Exception("Price is deleted");
                }

                var booking = new Booking
                {
                    UserId = bookingWithPatient.UserId,
                    PriceId = bookingWithPatient.PriceId,
                    MethodId = bookingWithPatient.MethodId,
                    AppointmentTime = bookingWithPatient.AppointmentTime,
                    StatusId = bookingWithPatient.StatusId,
                    Date = bookingWithPatient.Date,
                };
                _context.Bookings.Add(booking);
                _context.SaveChanges();

                foreach (var x in bookingWithPatient.patients)
                {
                    _context.Add(new Patient
                    {
                        BookingId = booking.BookingId,
                        PatientId = x.PatientId,
                        SampleId = x.SampleId,
                        FullName = x.FullName,
                        BirthDate = x.BirthDate,
                        Gender = x.Gender,
                        IdentifyId = x.IdentifyId,
                        HasTestedDna = x.HasTestedDna,
                        Relationship = x.Relationship
                    });
                }
                _context.SaveChanges();
                transaction.Commit();
                return booking.BookingId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
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
                    PatientId = patient.PatientId,
                    BookingId = patient.BookingId,
                    SampleId = patient.SampleId,
                    FullName = patient.FullName,
                    BirthDate = patient.BirthDate,
                    Gender = patient.Gender,
                    IdentifyId = patient.IdentifyId,
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
                throw;
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
                existingPatient.SampleId = patient.SampleId;
                existingPatient.FullName = patient.FullName;
                existingPatient.BirthDate = patient.BirthDate;
                existingPatient.Gender = patient.Gender;
                existingPatient.IdentifyId = patient.IdentifyId;
                existingPatient.HasTestedDna = patient.HasTestedDna;
                existingPatient.Relationship = patient.Relationship;

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
                throw;
            }
        }


    }
}
