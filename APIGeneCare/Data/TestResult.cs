using System;
using System.Collections.Generic;

namespace APIGeneCare.Data;

public partial class TestResult
{
    public int ResultId { get; set; }

    public int? BookingId { get; set; }

    public DateTime? Date { get; set; }

    public string? ResultSummary { get; set; }

    public virtual Booking? Booking { get; set; }
}
