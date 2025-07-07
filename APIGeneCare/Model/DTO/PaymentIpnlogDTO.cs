namespace APIGeneCare.Model.DTO;

public class PaymentIpnlogDTO
{
    public long IpnlogId { get; set; }

    public long PaymentId { get; set; }

    public string RawData { get; set; } = null!;

    public DateTime ReceivedAt { get; set; }

    public string Status { get; set; } = null!;
}
