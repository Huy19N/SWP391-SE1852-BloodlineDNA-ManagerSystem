using System;
using System.Collections.Generic;

namespace APIGeneCare.Model.DTO;

public partial class SampleDTO
{
    public int SampleId { get; set; }

    public int? BookingId { get; set; }

    public int? PatientId { get; set; }

    public DateTime? Date { get; set; }

    public string? SampleVariant { get; set; }

    public int? CollectBy { get; set; }

    public int? DeliveryMethodId { get; set; }

    public string? Status { get; set; }
}
