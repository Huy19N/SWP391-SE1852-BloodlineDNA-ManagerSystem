namespace APIGeneCare.Entities;

public partial class PaymentIpnlog
{
    public long IpnlogId { get; set; }

    public string PaymentId { get; set; } = null!;

    public string RawData { get; set; } = null!;

    public DateTime ReceivedAt { get; set; }

    public string TransactionStatus { get; set; } = null!;

    public string ResponseCode { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;
}
