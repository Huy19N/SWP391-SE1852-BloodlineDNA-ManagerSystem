using System;
using System.Collections.Generic;

namespace APIGeneCare.Data;

public partial class Sample
{
    public int SampleId { get; set; }

    public int? BookingId { get; set; }

    public DateTime? Date { get; set; }

    public string? SampleVariant { get; set; }

    public string? CollectBy { get; set; }

    public string? DeliveryMethod { get; set; }

    public string? Status { get; set; }

    public virtual Booking? Booking { get; set; }
}
