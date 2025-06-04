using System.Data;
using GeneCare.Models.Utils;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models
{
    public class Duration
    {
        private int? _durationId;
        private String? _durationName;
        private DateTime? _time;

        #region Properties
        public Duration() { }
        public Duration(int durationId, String durationName, DateTime time)
        {
            _durationId = durationId;
            _durationName = durationName;
            _time = time;
        }
        #endregion
        #region Getters and Setters
        public int? DurationId
        {
            get { return _durationId; }
            set { _durationId = value; }
        }
        public String? DurationName
        {
            get { return _durationName; }
            set { _durationName = value; }
        }
        public DateTime? Time
        {
            get { return _time; }
            set { _time = value; }
        }
        #endregion
        #region DAO
        public Duration? getDuration(int? durationId, String? durationName)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "SELECT * FROM Durations WHERE DurationID = @DurationId OR DurationName like @DurationName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@DurationId", SqlDbType.Int).Value = durationId;
                    cmd.Parameters.Add("@DurationName", SqlDbType.NVarChar).Value = durationName;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _durationId = reader.GetInt32("DurationID");
                            _durationName = reader.GetString("DurationName");
                            _time = reader.GetDateTime("Time");
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
