// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class VerifyEmailDTO
{
    public string Email { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public string? Key { get; set; }
}
