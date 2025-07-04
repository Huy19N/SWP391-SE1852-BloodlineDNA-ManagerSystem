using System;
using System.Collections.Generic;

namespace APIGeneCare.Entities;

public partial class PaymentIpnlog
{
    public long IpnlogId { get; set; }

    public long PaymentId { get; set; }

    public string RawData { get; set; } = null!;

    public DateTime ReceivedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;
}
