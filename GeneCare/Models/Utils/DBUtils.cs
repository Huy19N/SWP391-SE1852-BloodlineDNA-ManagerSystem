using Microsoft.Data.SqlClient;

namespace GeneCare.Models.Utils
{
    public class DBUtils
    {
        private const String connection = "Data Source=.;Initial Catalog=GeneCare;User ID=sa;Password=12345;TrustServerCertificate=True";

        public DBUtils() { }
        public SqlConnection getConnection()
        {
            return new SqlConnection(connection);
        }
    }
}
