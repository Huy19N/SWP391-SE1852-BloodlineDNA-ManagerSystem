// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class UserDTO
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string? FullName { get; set; }

    public int? IdentifyId { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public DateTime LastPwdChange { get; set; }

    public string? IPAddress { get; set; }

    public string? UserAgent { get; set; }

}
