using System.Data;
using GeneCare.Models.Utils;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models
{
    public class Blog
    {
        private int _blogId;
        private Users _user;
        private String _title;
        private String _content;
        private DateTime _date;

        #region Properties
        public Blog()
        {
            _user = new Users();
            _title = string.Empty;
            _content = string.Empty;
        }
        public Blog(int blogId, Users user, String title, String content, DateTime date)
        {
            _blogId = blogId;
            _user = user;
            _title = title;
            _content = content;
            _date = date;
        }
        #endregion
        #region Getters and Setters
        public int BlogId
        {
            get { return _blogId; }
            set { _blogId = value; }
        }
        public Users User
        {
            get { return _user; }
            set { _user = value; }
        }
        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public String Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        #endregion

        #region DAO
        public Blog? GetBlog(int? blogId, String? title)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "SELECT * FROM Blogs WHERE BlogID = @BlogId OR Title LIKE @Title";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@BlogId", SqlDbType.Int).Value = blogId;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _blogId = reader.GetInt32("BlogID");
                            _user.UserId = reader.GetInt32("UserID");
                            _title = reader.GetString("Title");
                            _content = reader.GetString("Content");
                            _date = reader.GetDateTime("Date");
                            return this;
                        }
                    }
                }
            }
            return null;
        }

        public Boolean AddBlog(Blog blog)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "INSERT INTO Blogs (UserID, Title, Content, Date) VALUES (@UserId, @Title, @Content, @Date)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = blog.User.UserId;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = blog.Title;
                    cmd.Parameters.Add("@Content", SqlDbType.NVarChar).Value = blog.Content;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = blog.Date;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;   
                }
            }
        }

        public Boolean UpdateBlog(Blog blog)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "UPDATE Blogs SET UserID = @UserId, Title = @Title, Content = @Content, Date = @Date WHERE BlogID = @BlogId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@BlogId", SqlDbType.Int).Value = blog.BlogId;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = blog.User.UserId;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = blog.Title;
                    cmd.Parameters.Add("@Content", SqlDbType.NVarChar).Value = blog.Content;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = blog.Date;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion
    }
}
