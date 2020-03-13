using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sp.iot.core;

namespace sp.iot.server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TankController : ControllerBase
    {

        private readonly ILogger<TankController> _logger;
        private readonly IConfiguration _config;

        private readonly IDatabase _database;

        public TankController(ILogger<TankController> logger, IConfiguration config, IDatabase database)
        {
            _logger = logger;
            _config = config;
            _database = database;
        }

        [HttpGet()]
        [ProducesErrorResponseType(typeof(void))]
        [ProducesResponseType(typeof(Tank[]), 200)]
        public IEnumerable<Tank> GetTanks([FromQuery(Name = "type")] TankType type)
        {
            List<Tank> returnValue = new List<Tank>();

            var conn = new SqliteConnectionStringBuilder();
            conn.DataSource = _config.GetValue<string>("Database:File");

            using (var connection = _database.GetConnection())
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select Tanks.*, LevelSensors.Percentage from Tanks left join LevelSensors on Tanks.LevelSensor = LevelSensors.ID";

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

        [HttpGet("Monitoring")]
        public IEnumerable<TankMonitoring> GetMonitoring()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new TankMonitoring
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.Now.AddDays(-index),
                FinishDate = DateTime.Now.AddDays(+index),
                Consumption = index * 11.5,
            })
            .ToArray();
        }
    }
}
