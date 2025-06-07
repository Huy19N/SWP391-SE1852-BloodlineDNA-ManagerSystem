using System;
using System.Collections.Generic;

namespace APIGeneCare.Data;

public partial class Duration
{
    public int DurationId { get; set; }

    public string? DurationName { get; set; }

    public TimeOnly? Time { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<ServicePrice> ServicePrices { get; set; } = new List<ServicePrice>();
}
