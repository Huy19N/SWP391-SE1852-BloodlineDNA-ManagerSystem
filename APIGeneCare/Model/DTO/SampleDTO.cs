using System;
using System.Collections.Generic;

namespace APIGeneCare.Model.DTO;

public partial class SampleDTO
{
    public int SampleId { get; set; }

    public int? DeliveryMethodId { get; set; }

    public string? SampleName { get; set; }
}
