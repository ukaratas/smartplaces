using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace sp.iot.core
{
    public class Database : IDatabase
    {
        private readonly IConfiguration _config;

        public SqliteConnection Connection { get; set; }

        public Database(IConfiguration config)
        {
            _config = config;
        }

        public void Open()
        {
            var conn = new SqliteConnectionStringBuilder();
            conn.DataSource = _config.GetValue<string>("Database:File");

            Connection = new SqliteConnection(conn.ConnectionString);
            Connection.Open();
        }

        public void Close()
        {
            Connection.Close();
        }

        public SqliteConnection GetConnection()
        {
            var conn = new SqliteConnectionStringBuilder();
            conn.DataSource = _config.GetValue<string>("Database:File");
            return new SqliteConnection(conn.ConnectionString);
        }

        public SqliteDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, null);
        }

        public SqliteDataReader ExecuteReader(string commandText, List<SqliteParameter> parameters)
        {
            if (Connection.State != ConnectionState.Open) Connection.Open();

            SqliteCommand command = Connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null)
                parameters.ForEach(item => command.Parameters.Add(item));
            return command.ExecuteReader();
        }
    }
}