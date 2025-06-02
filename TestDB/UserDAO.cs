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
        public UserDTO getUser(String Email, String Password)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                cmd.Parameters["@Email"].Value = Email;

                cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
                cmd.Parameters["@Password"].Value = Password;
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
