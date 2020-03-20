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


        public void SaveItem(
            Guid? id,
            string getQuery,
            string insertQuery,
            string updateQuery,
            List<SaveItemProperty> properties,
            Action<string> logCallback
            )
        {
            bool isRequiredValuesSuplied = true;

            properties.ForEach(item =>
            {
                if (item.IsRequired && item.Value == null)
                {
                    isRequiredValuesSuplied = false;
                    logCallback(string.Format("Property '{0}' is required but not suplied.", item.Name));
                }
            });

            if (!isRequiredValuesSuplied) return;

            if (id == null) id = Guid.NewGuid();

            SqliteDataReader reader = ExecuteReader(
                    getQuery,
                    new List<SqliteParameter>() { new SqliteParameter("Id", id.ToString()) });

            var saveParameters = new List<SqliteParameter>();
            saveParameters.Add(new SqliteParameter("Id", id.ToDBString()));

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
                      object oldValue = DBNull.Value;
                      if (oldRecordReader != null) oldValue = oldRecordReader.GetValue(oldRecordReader.GetOrdinal(item.Name));

                      switch (item.Value)
                      {
                          case null:
                              updateParameters.Add(new SqliteParameter(item.Name, oldValue));
                              break;
                          case string stringValue:
                              if (stringValue != oldValue.ToString())
                              {
                                  hasChange = true;
                                  if (oldRecordReader != null) logCallback(string.Format("Property '{0}' is changed. Item will be updated.", item.Name));
                                  updateParameters.Add(new SqliteParameter(item.Name, stringValue));
                              }
                              else updateParameters.Add(new SqliteParameter(item.Name, oldValue));

                              break;
                          case Guid guidValue:
                              if (oldValue == DBNull.Value)
                              {
                                  hasChange = true;
                                  updateParameters.Add(new SqliteParameter(item.Name, guidValue));
                                  if (oldRecordReader != null) logCallback(string.Format("Property '{0}' is changed. Item will be updated.", item.Name));
                              }
                              else if (guidValue != Guid.Parse(oldValue.ToString()))
                              {
                                  hasChange = true;
                                  updateParameters.Add(new SqliteParameter(item.Name, guidValue));
                                  if (oldRecordReader != null) logCallback(string.Format("Property '{0}' is changed. Item will be updated.", item.Name));
                              }
                              break;
                          case double doubleValue:
                              if (oldValue == DBNull.Value || doubleValue != (double)oldValue)
                              {
                                  hasChange = true;
                                  if (oldRecordReader != null) logCallback(string.Format("Property '{0}' is changed. Item will be updated.", item.Name));
                                  updateParameters.Add(new SqliteParameter(item.Name, doubleValue));
                              }
                              else updateParameters.Add(new SqliteParameter(item.Name, oldValue));
                              break;
                          case int intValue:
                              if (oldValue != DBNull.Value && intValue != (int)oldValue)
                              {
                                  hasChange = true;
                                  if (oldRecordReader != null) logCallback(string.Format("Property '{0}' is changed. Item will be updated.", item.Name));
                                  updateParameters.Add(new SqliteParameter(item.Name, intValue));
                              }
                              else updateParameters.Add(new SqliteParameter(item.Name, oldValue));
                              break;
                          default:
                              var baseType = item.Value.GetType().BaseType;
                              if (baseType.FullName == "System.Enum")
                              {
                                  if (oldValue != DBNull.Value && (int)(long)oldValue != (int)item.Value)
                                  {
                                      hasChange = true;
                                      if (oldRecordReader != null) logCallback(string.Format("Property '{0}' is changed. Item will be updated.", item.Name));
                                  }

                                  if ((int)item.Value == 0)
                                      updateParameters.Add(new SqliteParameter(item.Name, oldValue));
                                  else
                                      updateParameters.Add(new SqliteParameter(item.Name, item.Value));
                              }
                              break;
                      }
                  });

            return hasChange;
        }
    }

    public class SaveItemProperty
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public bool IsRequired { get; set; }
    }




}