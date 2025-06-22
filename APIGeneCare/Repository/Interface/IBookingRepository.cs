// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;

namespace APIGeneCare.Repository.Interface
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAllBookingsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<Booking> GetAllBookings();
        Booking? GetBookingById(int id);
        bool CreateBooking(Booking booking);
        bool UpdateBooking(Booking booking);
        bool DeleteBookingById(int id);
    }
}
