using System;
using System.Collections.Generic;

namespace APIGeneCare.Model.DTO;

public class PatientDTO
{
    public int PatientId { get; set; }

    public int BookingId { get; set; }

    public int? SampleId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string? IdentifyId { get; set; }

    public bool HasTestedDna { get; set; }

    public string? Relationship { get; set; }
}
