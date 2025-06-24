using System;
using System.Collections.Generic;

namespace APIGeneCare.Entities;

public partial class Patient
{
    public int PatientId { get; set; }

    public int BookingId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string? IdentifyId { get; set; }

    public string SampleType { get; set; } = null!;

    public bool HasTestedDna { get; set; }

    public string? Relationship { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual ICollection<Sample> Samples { get; set; } = new List<Sample>();
}
