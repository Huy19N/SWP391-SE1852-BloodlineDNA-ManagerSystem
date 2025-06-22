using System;
using System.Collections.Generic;

namespace APIGeneCare.Entities;

public partial class Patient
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

    public virtual Booking? Booking { get; set; }
}
