// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class PatientDTO
{
    public int PatientId { get; set; }

    public int? BookingId { get; set; }

    public string? FullName { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? IdentifyId { get; set; }

    public string? SampleType { get; set; }

    public bool? HasTestedDna { get; set; }

    public string? Relationship { get; set; }
}
