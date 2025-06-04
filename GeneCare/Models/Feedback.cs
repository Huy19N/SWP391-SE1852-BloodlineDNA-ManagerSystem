using System.Data;
using GeneCare.Models.Utils;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models
{
    public class Feedback
    {
        private int _feedbackId;
        private Users _user;
        private Service _service;
        private DateTime _createAt;
        private String _comment;
        private int _rating;

        #region Properties
        public Feedback()
        {
            _user = new Users();
            _service = new Service();
            _comment = string.Empty;
        }
        public Feedback(int feedbackId, Users user, Service service, DateTime createAt, String comment, int rating)
        {
            _feedbackId = feedbackId;
            _user = user;
            _service = service;
            _createAt = createAt;
            _comment = comment;
            _rating = rating;
        }
        #endregion
        #region Getters and Setters
        public int FeedbackId
        {
            get { return _feedbackId; }
            set { _feedbackId = value; }
        }
        public Users User
        {
            get { return _user; }
            set { _user = value; }
        }
        public Service Service
        {
            get { return _service; }
            set { _service = value; }
        }
        public DateTime CreateAt
        {
            get { return _createAt; }
            set { _createAt = value; }
        }
        public String Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        public int Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }
        #endregion
        #region DAO
        public Feedback? GetFeedback(int? feedbackId, int? userId, int? serviceId)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "SELECT * FROM Feedback WHERE FeedbackID = @FeedbackId OR UserID = @UserId OR ServiceID = @ServiceId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@FeedbackId", SqlDbType.Int).Value = feedbackId;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@ServiceId", SqlDbType.Int).Value = serviceId;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _feedbackId = reader.GetInt32("FeedbackID");
                            _user.UserId = reader.GetInt32("UserID");
                            _service.ServiceId = reader.GetInt32("ServiceID");
                            _createAt = reader.GetDateTime("CreateAt");
                            _comment = reader.GetString("Comment");
                            _rating = reader.GetInt32("Rating");
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
