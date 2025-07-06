namespace APIGeneCare.Model.DTO;

public partial class FeedbackDTO
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public int ServiceId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Comment { get; set; }

    public int Rating { get; set; }
}
