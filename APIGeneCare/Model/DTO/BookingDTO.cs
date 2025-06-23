// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace APIGeneCare.Model.DTO;

public partial class BookingDTO
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? DurationId { get; set; }

    public int? ServiceId { get; set; }

    public int? MethodId { get; set; }

    public DateTime? AppointmentTime { get; set; }

    public int? StatusId { get; set; }

    public DateTime? Date { get; set; }
}
