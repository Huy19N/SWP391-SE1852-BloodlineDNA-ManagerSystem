using APIGeneCare.Model;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IPatientRepository
    {
        IEnumerable<PatientDTO> GetAllPatientsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<PatientDTO> GetAllPatients();
        PatientDTO? GetPatientById(int id);
        bool CreatePatientWithBooking(BookingWithPatient bookingWithPatient);
        bool CreatePatient(PatientDTO patient);
        bool UpdatePatient(PatientDTO patient);
        bool DeletePatientById(int id);
    }
}
