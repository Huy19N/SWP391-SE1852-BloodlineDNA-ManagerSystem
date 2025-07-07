namespace APIGeneCare.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public int ServiceId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Comment { get; set; }

    public int Rating { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
