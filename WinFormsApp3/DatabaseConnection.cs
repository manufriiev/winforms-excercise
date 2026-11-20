
using Microsoft.Data.SqlClient;

namespace WinFormsApp3
{
    internal class DatabaseConnection
    {
        private SqlConnection connection;

        public DatabaseConnection()
        {
            string connectionString =
    "Server=(localdb)\\MSSQLLocalDB;Database=winforms1;TrustServerCertificate=True;";

            connection = new SqlConnection(connectionString);
        }

        public SqlConnection getConnection()
        {
            return connection;
        }
    }
}
