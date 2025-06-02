using Microsoft.Data.SqlClient;

namespace GeneCare.Models.Utils
{
    public class DBUtils
    {
        private const  String serverName = "(local)";
        private const  String dbName = "GeneCare";
        private const  String portNumber = "1433";
        private const  String instance = ""; //LEAVE THIS ONE EMPTY IF YOUR SQL IS A SINGLE INSTANCE
        private const  String userID = "sa";
        private const  String password = "12345";

        private const String connection = "Data Source=(local);Initial Catalog=GeneCare;Integrated Security=True;TrustServerCertificate=True";
        
        public DBUtils() { }
        public SqlConnection getConnection()
        {
            if (serverName.Equals("(local)"))
            {
                var connection = $"Data Source={serverName};Initial Catalog={dbName};Integrated Security=True;TrustServerCertificate=True";

            }
            else if (String.IsNullOrWhiteSpace(instance))
            {
                var connection = $"Data Source={serverName}:{portNumber};Initial Catalog={dbName};User ID={userID};Password={password};TrustServerCertificate=True";

            }
            else
            {
                var connection = $"Data Source={serverName}:{portNumber}\\{instance};Initial Catalog={dbName};User ID={userID};Password={password};TrustServerCertificate=True";

            }
            return new SqlConnection(connection);
        }
    }
}
