// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class FeedbackDTO
{
    public int FeedbackId { get; set; }

    public int? UserId { get; set; }

    public int? ServiceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Comment { get; set; }

    public int? Rating { get; set; }
}
