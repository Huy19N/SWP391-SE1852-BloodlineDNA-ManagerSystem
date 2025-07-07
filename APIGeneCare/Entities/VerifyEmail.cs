using System;
using System.Collections.Generic;

namespace APIGeneCare.Entities;

public partial class VerifyEmail
{
    public string Email { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public string? Key { get; set; }
}
