namespace APIGeneCare.Entities;

public partial class TestResult
{
    public int ResultId { get; set; }

    public DateTime? Date { get; set; }

    public string? ResultSummary { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
