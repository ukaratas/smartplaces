using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace sp.iot.core
{
    public class Database : IDatabase
    {

        private readonly IConfiguration _config;

        public Database(IConfiguration config)
        {
            _config = config;
        }

        public SqliteConnection GetConnection()
        {

            var conn = new SqliteConnectionStringBuilder();
            conn.DataSource = _config.GetValue<string>("Database:File");

            return new SqliteConnection(conn.ConnectionString);

        }
    }
}