// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Entities;

public partial class Sample
{
    public int SampleId { get; set; }

    public int? BookingId { get; set; }

    public DateTime? Date { get; set; }

    public string? SampleVariant { get; set; }

    public int? CollectBy { get; set; }

    public int? DeliveryMethodId { get; set; }

    public string? Status { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual User? CollectByNavigation { get; set; }

    public virtual DeliveryMethod? DeliveryMethod { get; set; }
}
