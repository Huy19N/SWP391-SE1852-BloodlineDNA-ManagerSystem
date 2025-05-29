using System.Data.SqlClient;
using System.Text;

namespace GeneCare.Models.Utils
{
    public class DBUtils
    {
        private const string serverName = "localhost";
        private const string dbName = "GeneCare";
        private const string portNumber = "1433";
        private const string instance = ""; // Leave this one empty if your SQL is a single instance
        private const string userID = "sa";
        private const string password = "12345";

        public static SqlConnection GetConnection() // Changed 'Connection' to 'SqlConnection'
        {
            StringBuilder sb;
            string connectionString = $"Server={serverName},{portNumber}\\{instance};Database={dbName};User Id={userID};Password={password};";
            if (instance == null || instance.Trim().Equals(""))
            {

            }
            // Return a new SqlConnection object
            return new SqlConnection(connectionString);
        }
    }
}
