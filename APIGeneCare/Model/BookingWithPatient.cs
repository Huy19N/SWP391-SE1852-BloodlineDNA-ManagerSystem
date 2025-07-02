using APIGeneCare.Model.DTO;

namespace APIGeneCare.Model
{
    public class BookingWithPatient : BookingDTO
    {
        public ICollection<PatientDTO> patients { get; set; }
    }
}
