namespace GeneCare.Models.DTO
{
    public class BookingDAO
    {
        private int bookingId;
        private UserDTO user;
        private DurationDAO duration;
        private ServiceDAO service;
        private int status;
        private String method;
        private DateTime date;

        public BookingDAO() { }
        public BookingDAO(int bookingId, UserDTO user, DurationDAO duration, ServiceDAO service, int status, String method, DateTime date)
        {
            this.bookingId = bookingId;
            this.user = user;
            this.duration = duration;
            this.service = service;
            this.status = status;
            this.method = method;
            this.date = date;
        }

        public int BookingId
        {
            get { return bookingId; }
            set { bookingId = value; }
        }
        public UserDTO User
        {
            get { return user; }
            set { user = value; }
        }
        public DurationDAO Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public ServiceDAO Service
        {
            get { return service; }
            set { service = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public String Method
        {
            get { return method; }
            set { method = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; } 
        }
    }
}