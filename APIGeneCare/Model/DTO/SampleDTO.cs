// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class SampleDTO
{
    public int SampleId { get; set; }

    public int? BookingId { get; set; }

    public DateTime? Date { get; set; }

    public string? SampleVariant { get; set; }

    public int? CollectBy { get; set; }

    public int? DeliveryMethodId { get; set; }

    public string? Status { get; set; }
}
