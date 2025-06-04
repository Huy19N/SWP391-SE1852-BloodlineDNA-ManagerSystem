using Microsoft.Data.SqlClient;
using GeneCare.Models.Utils;
using System.Data;

namespace GeneCare.Models
{
    public class Role
    {
        private int _roleId;
        private string _roleName;

        #region Properties
        public Role() {
            _roleName = string.Empty;
        }
        public Role(int roleId, string roleName)
        {
            _roleId = roleId;
            _roleName = roleName;
        }
        #endregion

        #region Getters and Setters
        public int RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }
        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
        #endregion

        #region DAO
        public Role? getRole(int? roleId, String? roleName)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "SELECT * FROM Roles WHERE RoleID = @RoleId OR RoleName like @RoleName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;
                    cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = roleName;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _roleId = reader.GetInt32("RoleID");
                            _roleName = reader.GetString("RoleName");
                            return this;
                        }
                    }

                }
            }
            return null;
        }
        public Boolean addRole(String roleName)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "INSERT INTO Roles (RoleName) VALUES (@RoleName)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = roleName;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public Boolean deleteRole(int roleId)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "DELETE FROM Roles WHERE RoleID = @RoleId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public Boolean updateRole(int roleId, String roleName)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "UPDATE Roles SET RoleName = @RoleName WHERE RoleID = @RoleId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;
                    cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = roleName;
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public Dictionary<int,Role>? getRoleList()
        {
            Dictionary<int, Role> roles = new Dictionary<int, Role>();
            var query = "SELECT * FROM Roles";
            using (SqlConnection con = new DBUtils().getConnection())
            { 
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            var role = new Role()
                            {
                                RoleId = reader.GetInt32("RoleID"),
                                RoleName = reader.GetString("RoleName")
                            };
                            roles.Add(role.RoleId, role);
                        }
                    }
                }
            }
            return roles;
        }
        #endregion

    }
}
