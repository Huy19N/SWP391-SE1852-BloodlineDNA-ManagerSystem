using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using GeneCare.Models.Utils;
using Microsoft.Identity.Client;
using TestDB;

class Program
{

    static class SqlHelper
    {
       public static UserDTO GetUser()
        {
            String sql = "SELECT * FROM Users WHERE UserId = @UserId";
            using (SqlConnection con = new DBUtils().getConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.add
            }

                return null;
        } 
       static void main(String[] arg)
        {

        }
    }
}