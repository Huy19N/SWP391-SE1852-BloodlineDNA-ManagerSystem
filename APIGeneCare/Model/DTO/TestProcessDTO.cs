// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class TestProcessDTO
{
    public int ProcessId { get; set; }

    public int? BookingId { get; set; }

    public int? StepId { get; set; }

    public int? StatusId { get; set; }

    public string? Description { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
