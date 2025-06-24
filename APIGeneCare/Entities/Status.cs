using System;
using System.Collections.Generic;

namespace APIGeneCare.Entities;

public partial class Status
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<TestProcess> TestProcesses { get; set; } = new List<TestProcess>();
}
