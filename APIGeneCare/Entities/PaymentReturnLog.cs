namespace APIGeneCare.Entities;

public partial class PaymentReturnLog
{
    public long ReturnLogId { get; set; }

    public string PaymentId { get; set; } = null!;

    public string RawData { get; set; } = null!;

    public DateTime ReturnedAt { get; set; }

    public string TransactionStatus { get; set; } = null!;

    public string ResponseCode { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;
}
