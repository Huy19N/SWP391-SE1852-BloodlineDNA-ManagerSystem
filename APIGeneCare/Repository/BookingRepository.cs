using APIGeneCare.Entities;
using APIGeneCare.Model;
using APIGeneCare.Model.AppSettings;
using APIGeneCare.Model.DTO;
using APIGeneCare.Repository.Interface;
using Microsoft.Extensions.Options;


namespace APIGeneCare.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly GeneCareContext _context;
        private readonly AppSettings _appSettings;
        public static int PAGE_SIZE { get; set; } = 10;
        public BookingRepository(GeneCareContext context,
            IOptionsMonitor<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.CurrentValue;
        }
        public IEnumerable<BookingDTO> GetAllBookingsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            var allBooking = _context.Bookings.AsQueryable();
            #region Search by type
            if (!String.IsNullOrWhiteSpace(typeSearch) && !String.IsNullOrWhiteSpace(search))
            {
                if (typeSearch.Equals("bookingid", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int bookingid))
                        allBooking = _context.Bookings.Where(b => b.BookingId == bookingid);

                if (typeSearch.Equals("userid", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int userid))
                        allBooking = _context.Bookings.Where(b => b.UserId == userid);

                if (typeSearch.Equals("durationid", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int durationid))
                        allBooking = _context.Bookings.Where(b => b.DurationId == durationid);

                if (typeSearch.Equals("serviceid", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int serviceid))
                        allBooking = _context.Bookings.Where(b => b.ServiceId == serviceid);

                if (typeSearch.Equals("status", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int status))
                        allBooking = _context.Bookings.Where(b => b.StatusId == status);
                if (typeSearch.Equals("method", StringComparison.CurrentCultureIgnoreCase))
                    if (int.TryParse(search, out int method))
                        allBooking = _context.Bookings.Where(b => b.MethodId == method);

            }
            #endregion
            #region Sort by
            if (!String.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("bookingid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderBy(b => b.BookingId);

                if (sortBy.Equals("bookingid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderByDescending(b => b.BookingId);

                if (sortBy.Equals("userid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderBy(b => b.UserId);

                if (sortBy.Equals("userid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderByDescending(b => b.UserId);

                if (sortBy.Equals("durationid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderBy(b => b.DurationId);

                if (sortBy.Equals("durationid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderByDescending(b => b.DurationId);

                if (sortBy.Equals("serviceid_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderBy(b => b.ServiceId);

                if (sortBy.Equals("serviceid_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderByDescending(b => b.ServiceId);

                if (sortBy.Equals("status_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderBy(b => b.Status);

                if (sortBy.Equals("status_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderByDescending(b => b.Status);

                if (sortBy.Equals("method_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderBy(b => b.Method);

                if (sortBy.Equals("method_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderByDescending(b => b.Method);

                if (sortBy.Equals("date_asc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderBy(b => b.Date);

                if (sortBy.Equals("date_desc", StringComparison.CurrentCultureIgnoreCase))
                    allBooking = allBooking.OrderByDescending(b => b.Date);

            }
            #endregion

            var result = PaginatedList<Booking>.Create(allBooking, page ?? 1, PAGE_SIZE);

            return result.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                DurationId = b.DurationId,
                ServiceId = b.ServiceId,
                MethodId = b.MethodId,
                ResultId = b.ResultId,
                AppointmentTime = b.AppointmentTime,
                StatusId = b.StatusId,
                Date = b.Date,
            });
        }
        public IEnumerable<BookingDTO> GetAllBookings()
            => _context.Bookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                DurationId = b.DurationId,
                ServiceId = b.ServiceId,
                MethodId = b.MethodId,
                ResultId = b.ResultId,
                AppointmentTime = b.AppointmentTime,
                StatusId = b.StatusId,
                Date = b.Date,
            }).ToList();
        public BookingDTO? GetBookingById(int id)
            => _context.Bookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                UserId = b.UserId,
                DurationId = b.DurationId,
                ServiceId = b.ServiceId,
                MethodId = b.MethodId,
                ResultId = b.ResultId,
                AppointmentTime = b.AppointmentTime,
                StatusId = b.StatusId,
                Date = b.Date,
            }).FirstOrDefault(b => b.BookingId == id);
        public bool CreateBooking(BookingDTO booking)
        {
            var timeInfor = TimeZoneInfo.FindSystemTimeZoneById(_appSettings.TimeZoneId);
            
            if (booking == null)
            {
                return false;
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Bookings.Add(new Booking
                {
                    UserId = booking.UserId,
                    DurationId = booking.DurationId,
                    ServiceId = booking.ServiceId,
                    MethodId = booking.MethodId,
                    ResultId = booking.ResultId,
                    AppointmentTime = booking.AppointmentTime,
                    StatusId = booking.StatusId,
                    Date = TimeZoneInfo.ConvertTime(booking.Date??DateTime.UtcNow, timeInfor)
                });
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public bool UpdateBooking(BookingDTO booking)
        {
            if (booking == null)
            {
                return false;
            }
            var existingBooking = _context.Bookings.Find(booking.BookingId);
            if (existingBooking == null)
            {
                return false;
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                existingBooking.UserId = booking.UserId;
                existingBooking.DurationId = booking.DurationId;
                existingBooking.ServiceId = booking.ServiceId;
                existingBooking.StatusId = booking.StatusId;
                existingBooking.MethodId = booking.MethodId;
                existingBooking.ResultId = booking.ResultId;
                existingBooking.Date = booking.Date;

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;

            }
        }
        public bool DeleteBookingById(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return false;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Bookings.Remove(booking);

                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
