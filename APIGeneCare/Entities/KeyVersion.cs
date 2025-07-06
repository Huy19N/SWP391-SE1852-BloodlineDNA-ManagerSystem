namespace APIGeneCare.Entities;

public partial class KeyVersion
{
    public long KeyVersionId { get; set; }

    public long PaymentMethodId { get; set; }

    public string Version { get; set; } = null!;

    public string HashSecret { get; set; } = null!;

    public string TmnCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public bool IsActive { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
