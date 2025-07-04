using System;
using System.Collections.Generic;

namespace APIGeneCare.Model.DTO;

public class PaymentMethodDTO
{
    public long PaymentMethodId { get; set; }

    public string MethodName { get; set; } = null!;

    public string? Description { get; set; }

    public string EndpointUrl { get; set; } = null!;

    public string IconUrl { get; set; } = null!;
}
