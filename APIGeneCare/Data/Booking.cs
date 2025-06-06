using System;
using System.Collections.Generic;

namespace APIGeneCare.Data;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? DurationId { get; set; }

    public int? ServiceId { get; set; }

    public int? Status { get; set; }

    public string? Method { get; set; }

    public DateTime? Date { get; set; }

    public virtual Duration? Duration { get; set; }

    public virtual ICollection<Sample> Samples { get; set; } = new List<Sample>();

    public virtual Service? Service { get; set; }

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();

    public virtual User? User { get; set; }
}
