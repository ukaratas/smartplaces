using System;
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

            var conn = new SqliteConnectionStringBuilder();
            conn.DataSource = _config.GetValue<string>("Database:File");

            Connection = new SqliteConnection(conn.ConnectionString);
            Connection.Open();
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
            SqliteCommand command = Connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null)
                parameters.ForEach(item => command.Parameters.Add(item));

            var result = command.ExecuteScalar();

            if (result != null)
                return (T)result;
            else
                return default(T);
        }


        public void SaveItem(
            Guid id,
            string getQuery,
            string insertQuery,
            string updateQuery,
            List<SaveItemProperty> properties,
            Action<string> logCallback
            )
        {
            SqliteDataReader reader = ExecuteReader(
                    getQuery,
                    new List<SqliteParameter>() { new SqliteParameter("Id", id) });

            var saveParameters = new List<SqliteParameter>();
            saveParameters.Add(new SqliteParameter("Id", id));

            if (reader.Read())
            {
                logCallback("Items is exists");
                var hasChange = _buildSaveParameters(properties, reader, saveParameters, logCallback);
                if (hasChange)
                {
                    ExecuteScalar<int>(updateQuery, saveParameters);
                    logCallback("Item is updated.");
                }
            }
            else
            {
                logCallback("Items is NOT exists");
                _buildSaveParameters(properties, null, saveParameters, logCallback);
                ExecuteScalar<int>(insertQuery, saveParameters);
                logCallback("Items is created.");
            }
        }

        private bool _buildSaveParameters(List<SaveItemProperty> properties, SqliteDataReader oldRecordReader, List<SqliteParameter> updateParameters, Action<string> logCallback)
        {
            var hasChange = false;
            properties.ForEach(item =>
                  {
                      object oldValue = (oldRecordReader != null) ? oldRecordReader.GetValue(oldRecordReader.GetOrdinal(item.Name)) : DBNull.Value;
                      var hasFieldChange = false;
                      switch (item.Value)
                      {
                          case null:
                              updateParameters.Add(new SqliteParameter(item.Name, oldValue));
                              break;
                          case string stringValue:
                              updateParameters.Add(new SqliteParameter(item.Name, stringValue));

                              if (stringValue != oldValue.ToString()) hasFieldChange = true;

                              break;
                          case Guid guidValue:
                              updateParameters.Add(new SqliteParameter(item.Name, guidValue));

                              if ((oldValue == DBNull.Value && guidValue != Guid.Empty)
                                    || (oldValue is Guid && guidValue != (Guid)oldValue)) hasFieldChange = true;
                              break;
                          case double doubleValue:
                              updateParameters.Add(new SqliteParameter(item.Name, doubleValue));
                              if (oldValue != DBNull.Value && (double)oldValue != doubleValue) hasFieldChange = true;
                              break;
                          case int intValue:
                              updateParameters.Add(new SqliteParameter(item.Name, intValue));
                              if (oldValue != DBNull.Value && (long)oldValue != intValue) hasFieldChange = true;
                              break;
                          default:
                              var baseType = item.Value.GetType().BaseType;
                              if (baseType.FullName == "System.Enum")
                              {
                                  updateParameters.Add(new SqliteParameter(item.Name, item.Value));

                                  if ((oldValue == DBNull.Value && (int)item.Value != 0)
                                      || (oldValue != DBNull.Value && (long)oldValue != (long)(int)item.Value)) hasFieldChange = true;
                              }
                              else
                              {
                                  throw new NotSupportedException(string.Format("{0} : {1}", item.Name, item.Value.GetType()));
                              }
                              break;
                      }

                      if (hasFieldChange && oldRecordReader != null)
                      {
                          logCallback(string.Format("Property '{0}' is changed. Item will be updated.", item.Name));
                          hasChange = true;
                      }
                  });
            return hasChange;
        }
    }

    public class SaveItemProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }




}