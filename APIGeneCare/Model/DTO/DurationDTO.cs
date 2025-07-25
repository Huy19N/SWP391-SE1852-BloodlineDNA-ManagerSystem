// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class DurationDTO
{
    public int DurationId { get; set; }

    public string? DurationName { get; set; }

    public TimeOnly? Time { get; set; }

    public bool IsDeleted { get; set; }
}
