namespace APIGeneCare.Entities;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public int? BookingId { get; set; }

    public long PaymentMethodId { get; set; }

    public string? TransactionStatus { get; set; }

    public string? ResponseCode { get; set; }

    public string? TransactionNo { get; set; }

    public string? BankTranNo { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime PaymentDate { get; set; }

    public string? OrderInfo { get; set; }

    public string? SecureHash { get; set; }

    public string? RawData { get; set; }

    public bool HavePaid { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual ICollection<PaymentIpnlog> PaymentIpnlogs { get; set; } = new List<PaymentIpnlog>();

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual ICollection<PaymentReturnLog> PaymentReturnLogs { get; set; } = new List<PaymentReturnLog>();
}
