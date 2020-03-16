using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace sp.iot.core
{
    public class TankService : ITankService
    {
        private readonly IConfiguration _config;
        private readonly IDatabase _database;

        public TankService(IConfiguration config, IDatabase database)
        {
            _config = config;
            _database = database;
        }

        public Tank Get(Guid tankId)
        {
            Tank returnValue = null;

            SqliteDataReader reader;
            reader = _database.ExecuteReader(
                "select Tanks.*, LevelSensors.Percentage from Tanks left join LevelSensors on Tanks.LevelSensor = LevelSensors.ID where Tanks.ID = @TankId",
                new List<SqliteParameter>() { new SqliteParameter("TankId", tankId.ToString()) }
                );

            if (reader.Read())
            {
                returnValue = _bindReaderData(reader);
            }
            _database.Close();
            return returnValue;
        }

        List<Tank> ITankService.Get(TankType? type)
        {
            List<Tank> returnValue = new List<Tank>();

            SqliteDataReader reader;

            if (type == null)
            {
                reader = _database.ExecuteReader("select Tanks.*, LevelSensors.Percentage from Tanks left join LevelSensors on Tanks.LevelSensor = LevelSensors.ID");
            }
            else
            {
                reader = _database.ExecuteReader(
                    "select Tanks.*, LevelSensors.Percentage from Tanks left join LevelSensors on Tanks.LevelSensor = LevelSensors.ID where Tanks.Type = @Type",
                    new List<SqliteParameter>() { new SqliteParameter("Type", type) }
                    );
            }
            while (reader.Read())
            {
                returnValue.Add(_bindReaderData(reader));
            }
            _database.Close();
            
            return returnValue;
        }

        public SaveResponse<Tank> Save(TankSaveRequest request)
        {
            SaveResponse<Tank> returnItem = new SaveResponse<Tank>();

            returnItem.AddAction("Save progress requested");

            using (var connection = _database.GetConnection())
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select Tanks.*, LevelSensors.Percentage from Tanks left join LevelSensors on Tanks.LevelSensor = LevelSensors.ID where Tanks.ID = @TankId";
                command.Parameters.Add(new SqliteParameter("TankId", request.Id.ToString()));

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var Tank = _bindReaderData(reader);
                        returnItem.AddAction("Tank is exists. It will be updated");
                    }
                    else
                    {
                        returnItem.AddAction("Tank is not exists. It will be created as a new Tank");

                        if (request.LevelSensorId != null)
                        {
                            returnItem.AddAction("Level Sensor Id is suplied. If exists it is going to linked else is created.");

                            var command2 = connection.CreateCommand();
                            command2.CommandText = "select * from LevelSensors where ID = @Id";
                            command2.Parameters.Add(new SqliteParameter("@Id", request.LevelSensorId.ToString()));

                            using (SqliteDataReader reader2 = command2.ExecuteReader())
                            {
                                if (reader2.Read())
                                {
                                    returnItem.AddAction("Level Sensor is exists.");
                                }
                                else
                                {
                                    returnItem.AddAction("Level Sensor is NOT exists.");
                                }
                            }
                        }
                        else
                        {
                            returnItem.AddAction("Level Sensor Id is not suplied. It is going to created.");
                        }


                        if (request.FlowSensorId != null)
                        {
                            returnItem.AddAction("Flow Sensor Id is suplied. If exists it is going to linked else is created.");
                        }
                        else
                        {
                            returnItem.AddAction("Flow Sensor Id is not suplied. It is going to created.");
                        }


                        if (request.ValveSensorId != null)
                        {
                            returnItem.AddAction("Valve Sensor Id is suplied. If exists it is going to linked else is created.");
                        }
                        else
                        {
                            returnItem.AddAction("Valve Sensor Id is not suplied. It is going to created.");
                        }

                    }
                }
            }
            return returnItem;
        }

        private Tank _bindReaderData(SqliteDataReader reader)
        {
            Tank returnValue = new Tank
            {
                Id = Guid.Parse(reader.GetValue(reader.GetOrdinal("ID")).ToString()),
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                Type = (TankType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")),
                LevelAsPercentage = (double)reader.GetValue(reader.GetOrdinal("Percentage")),
                IsLevelMonitored = !string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("LevelSensor")).ToString()),
                IsConsumptionMonitored = !string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("FlowSensor")).ToString()),
                CanEmpty = !string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("EmptyValve")).ToString()),
            };
            return returnValue;
        }

    }
}