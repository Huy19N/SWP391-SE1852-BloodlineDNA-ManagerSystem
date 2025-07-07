using System;
using System.Collections.Generic;

namespace APIGeneCare.Entities;

public partial class PaymentMethod
{
    public long PaymentMethodId { get; set; }

    public string MethodName { get; set; } = null!;

    public string IconUrl { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
