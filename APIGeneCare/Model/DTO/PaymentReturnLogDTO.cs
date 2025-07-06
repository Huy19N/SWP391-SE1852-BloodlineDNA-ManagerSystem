namespace APIGeneCare.Model.DTO;

public class PaymentReturnLogDTO
{
    public long ReturnLogId { get; set; }

    public long PaymentId { get; set; }

    public string RawData { get; set; } = null!;

    public DateTime ReturnedAt { get; set; }

    public string Status { get; set; } = null!;

}
