using System.Data;
using GeneCare.Models.Utils;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models
{
    public class Service
    {
        private int _serviceId;
        private String _serviceName;
        private String _description;
        #region Properties
        public Service() {
            _serviceName = String.Empty;
            _description = String.Empty;
        }
        public Service(int serviceId, String serviceName, String description)
        {
            _serviceId = serviceId;
            _serviceName = serviceName;
            _description = description;
        }
        #endregion
        #region Getters and Setters
        public int ServiceId
        {
            get { return _serviceId; }
            set { _serviceId = value; }
        }
        public String ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
        #endregion
        #region DAO
        public Service? getService(int? serviceId, String? serviceName)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "SELECT * FROM Services WHERE ServiceID = @ServiceId OR ServiceName like @ServiceName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ServiceId", SqlDbType.Int).Value = serviceId;
                    cmd.Parameters.Add("@ServiceName", SqlDbType.NVarChar).Value = serviceName;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _serviceId = reader.GetInt32("ServiceID");
                            _serviceName = reader.GetString("ServiceName");
                            _description = reader.GetString("Description");
                            return this;
                        }
                    }
                }
            }
            return null;
        }
        public Boolean addService(String serviceName, String description)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "INSERT INTO Services (ServiceName, Description) VALUES (@ServiceName, @Description)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ServiceName", SqlDbType.NVarChar).Value = serviceName;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = description;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public Boolean updateService(int serviceId, String serviceName, String description)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "UPDATE Services SET ServiceName = @ServiceName, Description = @Description WHERE ServiceID = @ServiceId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ServiceId", SqlDbType.Int).Value = serviceId;
                    cmd.Parameters.Add("@ServiceName", SqlDbType.NVarChar).Value = serviceName;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = description;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public Boolean deleteService(int serviceId)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "DELETE FROM Services WHERE ServiceID = @ServiceId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@ServiceId", SqlDbType.Int).Value = serviceId;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion
    }
}
