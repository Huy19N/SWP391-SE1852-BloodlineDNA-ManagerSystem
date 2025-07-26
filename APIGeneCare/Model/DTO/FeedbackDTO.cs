namespace APIGeneCare.Model.DTO;

public partial class FeedbackDTO
{
    public int BookingId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Comment { get; set; }

    public int Rating { get; set; }
}
