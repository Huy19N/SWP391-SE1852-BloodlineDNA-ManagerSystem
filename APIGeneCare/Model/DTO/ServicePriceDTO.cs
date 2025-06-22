// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class ServicePriceDTO
{
    public int PriceId { get; set; }

    public int? ServiceId { get; set; }

    public int? DurationId { get; set; }

    public int? Price { get; set; }

}
