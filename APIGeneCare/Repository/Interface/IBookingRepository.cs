using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface IBookingRepository
    {
        IEnumerable<BookingDTO> GetAllBookingsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        IEnumerable<BookingDTO> GetAllBookings();
        BookingDTO? GetBookingById(int id);
        bool CreateBooking(BookingDTO booking);
        bool UpdateBooking(BookingDTO booking);
        bool DeleteBookingById(int id);
    }
}
