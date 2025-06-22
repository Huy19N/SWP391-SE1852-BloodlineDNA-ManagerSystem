// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class TestResult
{
    public int ResultId { get; set; }

    public int? BookingId { get; set; }

    public DateTime? Date { get; set; }

    public string? ResultSummary { get; set; }
}
