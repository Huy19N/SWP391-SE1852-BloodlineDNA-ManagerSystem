using System.Data;
using GeneCare.Models.Utils;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models
{
    public class Booking
    {
        private int _bookingId;
        private Users _user;
        private Duration _duration;
        private Service _service;
        private int _status;
        private String _method;
        private DateTime _date;

        #region Properties
        public Booking() {
            _user = new Users(); 
            _duration = new Duration();
            _service = new Service();
            _method = string.Empty;
        }
        public Booking(int bookingId, Users user, Duration duration, Service service, int status, String method, DateTime date)
        {
            _bookingId = bookingId;
            _user = user;
            _duration = duration;
            _service = service;
            _status = status;
            _method = method;
            _date = date;
        }
        #endregion

        #region Getters and Setters
        public int BookingId
        {
            get { return _bookingId; }
            set { _bookingId = value; }
        }
        public Users User
        {
            get { return _user; }
            set { _user = value; }
        }
        public Duration Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        public Service Service
        {
            get { return _service; }
            set { _service = value; }
        }
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public String Method
        {
            get { return _method; }
            set { _method = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        #endregion

        #region DAO
        public Booking? GetBooking(int? bookingId, String? method)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "SELECT * FROM Bookings WHERE BookingID = @BookingId OR Method LIKE @Method";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = bookingId;
                    cmd.Parameters.Add("@Method", SqlDbType.NVarChar).Value = method;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _bookingId = reader.GetInt32("BookingID");
                            _user = new Users().getUser(reader.GetInt32("UserID"), null, null);
                            _duration = new Duration().getDuration(reader.GetInt32("DurationID"), null);
                            _service = new Service().getService(reader.GetInt32("ServiceID"), null);
                            _status = reader.GetInt32("Status");
                            _method = reader.GetString("Method");
                            _date = reader.GetDateTime("Date");
                            return this;
                        }
                    }
                }
            }
            return null;
        }

        public Boolean AddBooking(Users user, Duration duration, Service service, int status, String method, DateTime date)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "INSERT INTO Bookings (UserID, DurationID, ServiceID, Status, Method, Date) VALUES (@UserID, @DurationID, @ServiceID, @Status, @Method, @Date)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserId;
                    cmd.Parameters.Add("@DurationID", SqlDbType.Int).Value = duration.DurationId;
                    cmd.Parameters.Add("@ServiceID", SqlDbType.Int).Value = service.ServiceId;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                    cmd.Parameters.Add("@Method", SqlDbType.NVarChar).Value = method;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = date;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Boolean UpdateBooking(int bookingId, Users user, Duration duration, Service service, int status, String method, DateTime date)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "UPDATE Bookings SET UserID = @UserID, DurationID = @DurationID, ServiceID = @ServiceID, Status = @Status, Method = @Method, Date = @Date WHERE BookingID = @BookingId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = bookingId;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserId;
                    cmd.Parameters.Add("@DurationID", SqlDbType.Int).Value = duration.DurationId;
                    cmd.Parameters.Add("@ServiceID", SqlDbType.Int).Value = service.ServiceId;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                    cmd.Parameters.Add("@Method", SqlDbType.NVarChar).Value = method;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = date;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Boolean DeleteBooking(int bookingId)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "DELETE FROM Bookings WHERE BookingID = @BookingId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = bookingId;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

    }
}
