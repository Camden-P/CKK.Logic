using CKK.DB.Interfaces;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace CKK.DB.UOW
{
    public class DatabaseConnectionFactory : IConnectionFactory // Connection to the database
    {
        // Method to return connection string
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        // Connection string to connect to database
        private readonly string connectionString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = StructuredProjectDB";

        // Get connection to database
        public IDbConnection GetConnection
        {
            get
            {
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var conn = factory.CreateConnection();
                conn.ConnectionString = connectionString;
                //conn.Open();
                return conn;
            }
        }
    }
}
