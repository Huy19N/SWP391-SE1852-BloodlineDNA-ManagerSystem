// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class RefreshTokenDTO
{
    public int TokenId { get; set; }

    public int? UserId { get; set; }

    public string? Token { get; set; }

    public string? JwtId { get; set; }

    public DateTime? IssueAt { get; set; }

    public DateTime? ExpiredAt { get; set; }
}
