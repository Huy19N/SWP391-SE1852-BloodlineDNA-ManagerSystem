using System.Data;
using GeneCare.Models.Utils;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models
{
    public class Samples
    {
        private int _sampleId;
        private Booking _booking;
        private DateTime _date;
        private String _sampleVariant;
        private String _collectedBy;
        private String _deliveryMethod;
        private String _status;

        #region Properties
        public Samples() {
            _booking = new Booking();
            _sampleVariant = string.Empty;
            _collectedBy = string.Empty;
            _deliveryMethod = string.Empty;
            _status = string.Empty;
        }
        public Samples(int sampleId, Booking booking, DateTime date, String sampleVariant, String collectedBy, String deliveryMethod, String status)
        {
            _sampleId = sampleId;
            _booking = booking;
            _date = date;
            _sampleVariant = sampleVariant;
            _collectedBy = collectedBy;
            _deliveryMethod = deliveryMethod;
            _status = status;
        }
        #endregion
        #region Getters and Setters
        public int SampleId
        {
            get { return _sampleId; }
            set { _sampleId = value; }
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
        public String SampleVariant
        {
            get { return _sampleVariant; }
            set { _sampleVariant = value; }
        }
        public String CollectedBy
        {
            get { return _collectedBy; }
            set { _collectedBy = value; }
        }
        public String DeliveryMethod
        {
            get { return _deliveryMethod; }
            set { _deliveryMethod = value; }
        }
        public String Status
        {
            get { return _status; }
            set { _status = value; }
        }
        #endregion

        #region DAO
        public Samples? getSample(int? sampleId, String? sampleVariant)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "SELECT * FROM Samples WHERE SampleID = @SampleId OR SampleVariant like @SampleVariant";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@SampleId", SqlDbType.Int).Value = sampleId;
                    cmd.Parameters.Add("@SampleVariant", SqlDbType.NVarChar).Value = sampleVariant;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _sampleId = reader.GetInt32("SampleID");
                            _booking = new Booking();
                            _date = reader.GetDateTime("Date");
                            _sampleVariant = reader.GetString("SampleVariant");
                            _collectedBy = reader.GetString("CollectedBy");
                            _deliveryMethod = reader.GetString("DeliveryMethod");
                            _status = reader.GetString("Status");
                            return this;
                        }
                    }
                }
            }
            return null;
        }

        public Boolean addSample(Booking booking, DateTime date, String sampleVariant, String collectedBy, String deliveryMethod, String status)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "INSERT INTO Samples (BookingID, Date, SampleVariant, CollectedBy, DeliveryMethod, Status) VALUES (@BookingID, @Date, @SampleVariant, @CollectedBy, @DeliveryMethod, @Status)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@BookingID", SqlDbType.Int).Value = booking.BookingId;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = date;
                    cmd.Parameters.Add("@SampleVariant", SqlDbType.NVarChar).Value = sampleVariant;
                    cmd.Parameters.Add("@CollectedBy", SqlDbType.NVarChar).Value = collectedBy;
                    cmd.Parameters.Add("@DeliveryMethod", SqlDbType.NVarChar).Value = deliveryMethod;
                    cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = status;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Boolean updateSample(int sampleId, String sampleVariant, String status)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "UPDATE Samples SET SampleVariant = @SampleVariant, Status = @Status WHERE SampleID = @SampleId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@SampleId", SqlDbType.Int).Value = sampleId;
                    cmd.Parameters.Add("@SampleVariant", SqlDbType.NVarChar).Value = sampleVariant;
                    cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = status;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Boolean deleteSample(int sampleId)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "DELETE FROM Samples WHERE SampleID = @SampleId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@SampleId", SqlDbType.Int).Value = sampleId;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion
    }
}
