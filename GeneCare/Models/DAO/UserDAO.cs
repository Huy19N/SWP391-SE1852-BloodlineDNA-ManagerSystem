

using Microsoft.Data.SqlClient;
using GeneCare.Models.DTO;
using GeneCare.Models.Utils;
using System.Collections.Generic;

namespace GeneCare.Models.DAO
{
    public class UserDAO
    {
        public UserDTO getUser(String email, String password)
        {
            UserDTO userDTO = null;
            String sql = "SELECT * FROM [User] WHERE Email = @Email AND Password = @Password";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Email", $"N'{email}'");
                    cmd.Parameters.AddWithValue("@Password", $"N'{password}'");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userDTO = new UserDTO();
                            userDTO.UserId = Convert.ToInt32(reader["UserID"]);
                            userDTO.RoleId = Convert.ToInt32(reader["RoleID"]);
                            userDTO.FullName = Convert.ToString(reader["FullName"]);
                            userDTO.Address = Convert.ToString(reader["Address"]);
                            userDTO.Email = Convert.ToString(reader["Email"]);
                            userDTO.Phone = Convert.ToString(reader["Phone"]);
                            userDTO.Password = Convert.ToString(reader["Password"]);
                        }
                    }
                }
            }   
            return userDTO;
        }

        public Boolean addUser(String email, String password)
        {
            String sql = "INSERT INTO Users(Email, Password) VALUES(@Email, @Password)";
            
            using(SqlConnection con = new DBUtils().getConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Email", $"N\"{email}\"");
                    cmd.Parameters.AddWithValue("@Password", $"N\"{password}\"");

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }   
        }
    }
}
