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
            var sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
            using (SqlConnection con = new DBUtils().getConnection())
            {

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
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
                                user.Role = new RoleDAO().getRole(Convert.ToInt32(reader["RoleId"]), null);
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
                }
                return null;
            }
        }

        public Boolean containsUser(String email)
        {
            var sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

            using (SqlConnection con = new DBUtils().getConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                    cmd.Parameters["@Email"].Value = email;

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            return false;
        }


        public Boolean addUser(String email, String password)
        {
            var sql = "INSERT INTO Users(Email, Password) VALUES(@Email, @Password)";

            using (SqlConnection con = new DBUtils().getConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar);

                    cmd.Parameters["@Email"].Value = email;
                    cmd.Parameters["@Password"].Value = password;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public Boolean updateUser(UserDTO user)
        {
            var sql = "UPDATE Users SET FullName = @FullName, Address = @Address, Phone = @Phone WHERE UserId = @UserId";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@FullName", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@UserId", SqlDbType.Int);


                    cmd.Parameters["@FullName"].Value = user.FullName;
                    cmd.Parameters["@Address"].Value = user.Address;
                    cmd.Parameters["@Phone"].Value = user.Phone;
                    cmd.Parameters["@UserId"].Value = user.UserId;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
