using System;
using System.Collections.Generic;

namespace APIGeneCare.Entities;

public partial class Sample
{
    public int SampleId { get; set; }

    public int? DeliveryMethodId { get; set; }

    public string? SampleName { get; set; }

    public virtual DeliveryMethod? DeliveryMethod { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
