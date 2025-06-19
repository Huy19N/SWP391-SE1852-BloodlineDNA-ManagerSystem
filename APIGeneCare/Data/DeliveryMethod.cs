// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Data;

public partial class DeliveryMethod
{
    public int DeliveryMethodId { get; set; }

    public string? DeliveryMethodName { get; set; }

    public virtual ICollection<Sample> Samples { get; set; } = new List<Sample>();
}
