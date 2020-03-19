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

            if (Connection == null)
            {
                //if (Connection.State != ConnectionState.Open) Connection.Open();

                var conn = new SqliteConnectionStringBuilder();
                conn.DataSource = _config.GetValue<string>("Database:File");

                Connection = new SqliteConnection(conn.ConnectionString);
            }
            if (Connection.State != ConnectionState.Open) Connection.Open();
        }

        public void Close()
        {
            if (Connection != null) Connection.Close();
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

            Open();

            SqliteCommand command = Connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null)
                parameters.ForEach(item => command.Parameters.Add(item));
            return command.ExecuteReader();
        }


        public T ExecuteScalar<T>(string commandText)
        {
            return ExecuteScalar<T>(commandText, null);
        }

        public T ExecuteScalar<T>(string commandText, List<SqliteParameter> parameters)
        {
            Open();
            SqliteCommand command = Connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null)
                parameters.ForEach(item => command.Parameters.Add(item));

            var result = command.ExecuteScalar();
            Close();
            if (result != null)
                return (T)result;
            else
                return default(T);
        }
    }
}