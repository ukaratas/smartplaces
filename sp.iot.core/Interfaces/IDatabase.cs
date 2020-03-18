using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IDatabase
    {
        void Open();

        void Close();

        SqliteConnection GetConnection();
        SqliteDataReader ExecuteReader(string commandText);

        SqliteDataReader ExecuteReader(string commandText, List<SqliteParameter> parameters);

        T ExecuteScalar<T>(string commandText);

        T ExecuteScalar<T>(string commandText, List<SqliteParameter> parameters);
    }

}