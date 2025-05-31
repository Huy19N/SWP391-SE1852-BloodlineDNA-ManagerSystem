using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TestDB;

namespace TestDB
{
    internal class UserDAO
    {
        public UserDTO GetUser(int UserId)
        {
            String sql = "SELECT * FROM [User] WHERE UserId = @UserId";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@UserId", SqlDbType.Int);
                cmd.Parameters["@UserId"].Value = UserId;
                try
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserDTO user = new UserDTO();
                            user.UserId = Convert.ToInt32(reader["UserId"]);
                            user.RoleId = Convert.ToInt32(reader["RoleId"]);
                            user.FullName = reader["FullName"].ToString();
                            user.Address = reader["Address"].ToString();
                            user.Email = reader["Email"].ToString();
                            user.Phone = reader["Phone"].ToString();
                            user.Password = reader["Password"].ToString();
                            return user;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error connecting to database: " + ex.Message);
                    return null;
                }

                return null;
            }
        }
    }
}
