namespace APIGeneCare.Entities;

public partial class PaymentMethod
{
    public long PaymentMethodId { get; set; }

    public string MethodName { get; set; } = null!;

    public string? Description { get; set; }

    public string EndpointUrl { get; set; } = null!;

    public string IconUrl { get; set; } = null!;

    public virtual ICollection<KeyVersion> KeyVersions { get; set; } = new List<KeyVersion>();
}
