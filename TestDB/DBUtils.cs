using Microsoft.Data.SqlClient;

namespace GeneCare.Models.Utils
{
    public class DBUtils
    {
        private const String connection = "Data Source=(local);Initial Catalog=GeneCare;Integrated Security=True;TrustServerCertificate=True";

        public DBUtils() { }
        public SqlConnection getConnection()
        {
            return new SqlConnection(connection);
        }
    }
}
