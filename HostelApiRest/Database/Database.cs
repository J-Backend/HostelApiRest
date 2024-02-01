using System.Data.SqlClient;

namespace HostelApiRest.Database
{
    public class Database : IDatabase
    {
        private readonly IConfiguration configuration;
        private string ConnectionStringName { get; set; } = "BookingADOString";
        public Database(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public SqlConnection GetConnection()
        {

            var connectionString = this.configuration.GetConnectionString(ConnectionStringName);

            var connection = new SqlConnection(connectionString);

            return connection;
        }
    }
}
