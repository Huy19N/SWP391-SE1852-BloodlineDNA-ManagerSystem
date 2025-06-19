// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Data;

public partial class TestProcess
{
    public int ProcessId { get; set; }

    public int? BookingId { get; set; }

    public int? StepId { get; set; }

    public int? StatusId { get; set; }

    public string? Description { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Status? Status { get; set; }

    public virtual TestStep? Step { get; set; }
}
