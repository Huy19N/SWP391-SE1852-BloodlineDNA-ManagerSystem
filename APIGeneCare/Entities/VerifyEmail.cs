namespace APIGeneCare.Entities;

public partial class VerifyEmail
{
    public string Otp { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsResetPwd { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiredAt { get; set; }
}
