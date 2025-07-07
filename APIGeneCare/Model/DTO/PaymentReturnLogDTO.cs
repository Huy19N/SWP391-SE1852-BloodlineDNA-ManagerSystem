namespace APIGeneCare.Model.DTO;

public class PaymentReturnLogDTO
{
    public long ReturnLogId { get; set; }

    public long PaymentId { get; set; }

    public string RawData { get; set; } = null!;

    public DateTime ReturnedAt { get; set; }

    public string TransactionStatus { get; set; } = null!;

    public string ResponseCode { get; set; } = null!;

}
