namespace APIGeneCare.Entities;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? ServiceType { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ServicePrice> ServicePrices { get; set; } = new List<ServicePrice>();
}
