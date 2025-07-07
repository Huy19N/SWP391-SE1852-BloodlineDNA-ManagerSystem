namespace APIGeneCare.Model.DTO;

public class PaymentMethodDTO
{
    public long PaymentMethodId { get; set; }

    public string MethodName { get; set; } = null!;

    public string IconUrl { get; set; } = null!;
}
