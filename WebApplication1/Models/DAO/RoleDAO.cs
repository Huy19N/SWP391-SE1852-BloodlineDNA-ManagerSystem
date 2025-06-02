using System.Data;
using GeneCare.Models.DTO;
using GeneCare.Models.Utils;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models.DAO
{
    public class RoleDAO
    {
        public RoleDTO getRole(int roleId, String? roleName)
        {
            var sql = "SELECT * FROM Role WHERE RoleId = @RoleId OR RoleName = @RoleName";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int);
                    cmd.Parameters["@RoleId"].Value = roleId;

                    cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar);
                    cmd.Parameters["@RoleName"].Value = roleName ?? string.Empty;

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        try
                        {
                            if (reader.Read())
                            {
                                var role = new RoleDTO();
                                role.RoleId = Convert.ToInt32(reader["RoleID"]);
                                role.RoleName = reader["RoleName"].ToString();
                                return role;
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine("Error connecting to database: " + ex.Message);
                            return null;
                        }
                        finally
                        {
                            con.Close();

                        }
                    }
                    return null;
                }
            }
        }



    }
}
