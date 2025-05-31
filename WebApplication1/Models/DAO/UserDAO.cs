using System.Data;
using GeneCare.Models.DTO;
using GeneCare.Models.Utils;
using Microsoft.Data;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models.DAO
{
    public class UserDAO
    {
        public UserDTO getUser(String Email, String Password)
        {
            String sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                cmd.Parameters["@Email"].Value = Password;

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
