namespace APIGeneCare.Entities;

public partial class RefreshToken
{
    public int TokenId { get; set; }

    public int? UserId { get; set; }

    public string? Token { get; set; }

    public string? JwtId { get; set; }

    public DateTime? IssueAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public virtual User? User { get; set; }
}
