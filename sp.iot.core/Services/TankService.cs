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

        List<Tank> ITankService.GetTanks(TankType? type)
        {
            List<Tank> returnValue = new List<Tank>();

            using (var connection = _database.GetConnection())
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();
                if (type == null)
                {
                    command.CommandText = "select Tanks.*, LevelSensors.Percentage from Tanks left join LevelSensors on Tanks.LevelSensor = LevelSensors.ID";
                }
                else
                {
                    command.CommandText = "select Tanks.*, LevelSensors.Percentage from Tanks left join LevelSensors on Tanks.LevelSensor = LevelSensors.ID where Tanks.Type = @Type";
                    command.Parameters.Add(new SqliteParameter("Type", type));
                }

                Tank item = null;

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = new Tank
                        {
                            Id = Guid.Parse(reader.GetValue(reader.GetOrdinal("ID")).ToString()),
                            Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                            Type = (TankType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")),
                            LevelAsPercentage = (double)reader.GetValue(reader.GetOrdinal("Percentage")),
                            IsLevelMonitored = !string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("LevelSensor")).ToString()),
                            IsConsumptionMonitored = !string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("FlowSensor")).ToString()),
                            CanEmpty = !string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("EmptyValve")).ToString()),
                        };
                        returnValue.Add(item);
                    }
                }
            }
            return returnValue;
        }
    }
}