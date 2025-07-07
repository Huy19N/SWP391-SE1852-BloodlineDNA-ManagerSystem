namespace APIGeneCare.Entities;

public partial class ServicePrice
{
    public int PriceId { get; set; }

    public int? ServiceId { get; set; }

    public int? DurationId { get; set; }

    public int? Price { get; set; }

    public virtual Duration? Duration { get; set; }

    public virtual Service? Service { get; set; }
}
