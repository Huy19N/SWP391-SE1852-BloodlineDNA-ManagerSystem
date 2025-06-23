// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class PaymentDTO
{
    public int PaymentId { get; set; }

    public int? BookingId { get; set; }

    public int? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }
}
