namespace APIGeneCare.Entities;

public partial class Payment
{
    public long PaymentId { get; set; }

    public int BookingId { get; set; }

    public long KeyVersionId { get; set; }

    public string TransactionId { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime PaymentDate { get; set; }

    public string? BankCode { get; set; }

    public string OrderInfo { get; set; } = null!;

    public string? ResponseCode { get; set; }

    public string SecureHash { get; set; } = null!;

    public string RawData { get; set; } = null!;

    public bool HavePaid { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual KeyVersion KeyVersion { get; set; } = null!;

    public virtual ICollection<PaymentIpnlog> PaymentIpnlogs { get; set; } = new List<PaymentIpnlog>();

    public virtual ICollection<PaymentReturnLog> PaymentReturnLogs { get; set; } = new List<PaymentReturnLog>();
}
