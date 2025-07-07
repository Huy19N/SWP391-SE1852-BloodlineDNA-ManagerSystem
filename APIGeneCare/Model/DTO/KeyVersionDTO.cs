namespace APIGeneCare.Model.DTO;

public class KeyVersionDTO
{
    public long KeyVersionId { get; set; }

    public long PaymentMethodId { get; set; }

    public string Version { get; set; } = null!;

    public string HashSecret { get; set; } = null!;

    public string TmnCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public bool IsActive { get; set; }
}
