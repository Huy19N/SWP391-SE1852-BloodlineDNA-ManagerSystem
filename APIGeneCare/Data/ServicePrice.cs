// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Data;

public partial class ServicePrice
{
    public int PriceId { get; set; }

    public int? ServiceId { get; set; }

    public int? DurationId { get; set; }

    public int? Price { get; set; }

    public virtual Duration? Duration { get; set; }

    public virtual Service? Service { get; set; }
}
