using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAllBookings(string? typeSearch, string? search, string? sortBy, int? page);
        Booking? GetBookingById(int id);
        bool CreateBooking(Booking booking);
        bool UpdateBooking(Booking booking);
        bool DeleteBooking(int id);
    }
}
