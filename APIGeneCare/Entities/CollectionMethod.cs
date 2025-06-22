using System;
using System.Collections.Generic;

namespace APIGeneCare.Entities;

public partial class CollectionMethod
{
    public int MethodId { get; set; }

    public string? MethodName { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
