// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class ServiceDTO
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? ServiceType { get; set; }

    public string? Description { get; set; }
}
