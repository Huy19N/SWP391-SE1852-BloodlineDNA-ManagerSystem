using System.Data;
using GeneCare.Models.Utils;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models
{
    public class TestResult
    { 
        private int _resultId;
        private Booking _booking;
        private DateTime _date;
        private String _resultSummary;
        #region Properties
        public TestResult()
        {
            _booking = new Booking();
            _resultSummary = string.Empty;
        }
        public TestResult(int resultId, Booking booking, DateTime date, String resultSummary)
        {
            _resultId = resultId;
            _booking = booking;
            _date = date;
            _resultSummary = resultSummary;
        }
        #endregion

        #region Getters and Setters
        public int ResultId
        {
            get { return _resultId; }
            set { _resultId = value; }
        }
        public Booking Booking
        {
            get { return _booking; }
            set { _booking = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public String ResultSummary
        {
            get { return _resultSummary; }
            set { _resultSummary = value; }
        }
        #endregion

        #region DAO
        public TestResult? GetTestResult(int? resultId, String? resultSummary)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "SELECT * FROM TestResults WHERE ResultID = @ResultId OR ResultSummary LIKE @ResultSummary";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ResultId", SqlDbType.Int).Value = resultId ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@ResultSummary", SqlDbType.NVarChar).Value = resultSummary ?? (object)DBNull.Value;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _resultId = reader.GetInt32("ResultID");
                            _booking = new Booking().GetBooking(reader.GetInt32("BookingID"), null);
                            _date = reader.GetDateTime("Date");
                            _resultSummary = reader.GetString("ResultSummary");
                            return this;
                        }
                    }
                }
            }
            return null;
        }
#endregion
    }
}
