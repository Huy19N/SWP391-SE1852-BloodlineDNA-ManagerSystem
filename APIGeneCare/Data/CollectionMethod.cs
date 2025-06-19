// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Data;

public partial class CollectionMethod
{
    public int MethodId { get; set; }

    public string? MethodName { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
