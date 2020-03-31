using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IDatabase
    {
        SqliteDataReader ExecuteReader(string commandText);

        SqliteDataReader ExecuteReader(string commandText, List<SqliteParameter> parameters);

        T ExecuteScalar<T>(string commandText);

        T ExecuteScalar<T>(string commandText, List<SqliteParameter> parameters);

        void SaveItem(Guid id, string getQuery, string insertQuery, string updateQuery, List<SaveItemProperty> properties, Action<string> logCallback);
    }

}