using Microsoft.Data.SqlClient;

namespace TestDB
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
