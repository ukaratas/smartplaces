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

            SqliteDataReader reader = _database.ExecuteReader(
                "select Tanks.*, LevelSensors.Percentage from Tanks left join LevelSensors on Tanks.LevelSensor = LevelSensors.ID where Tanks.ID = @TankId",
                new List<SqliteParameter>() { new SqliteParameter("TankId", tankId.ToString()) }
                );

            if (reader.Read())
            {
                returnValue = _bindReaderData(reader);
            }

            reader.Close();
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

            returnItem.AddAction("Save progress started.");

            _saveGadget(
                request.LevelSensorId,
                request.LevelSensorType,
                request.LevelSensorName,
                request.LevelSensorConnectionPort,
                (log) => { returnItem.AddAction("Level Sensor : " + log); }
                );

            _saveGadget(
                request.FlowSensorId,
                request.FlowSensorType,
                request.FlowSensorName,
                request.FlowSensorConnectionPort,
                (log) => { returnItem.AddAction("Flow Sensor : " + log); }
                );

            _saveGadget(
                request.ValveId,
                request.ValveType,
                request.ValveName,
                request.ValveConnectionPort,
                (log) => { returnItem.AddAction("Valve : " + log); }
                );


            _saveTank(
                request.Id,
                request.Type,
                request.Name,
                request.LevelSensorId,
                request.FlowSensorId,
                request.ValveId,
                request.PercentageRatio,
                request.Unit,
                (log) => { returnItem.AddAction(log); }
             );

            _database.Close();
            return returnItem;
        }


        private void _saveTank(Guid? id, TankType? tankType, string name, Guid? levelSensor, Guid? flowSensor, Guid? valve, double ratio, UnitType? unitType, Action<string> actionLogCallback)
        {

            if (unitType != null && !Enum.IsDefined(typeof(UnitType), unitType)) unitType = null;
            if (tankType != null && !Enum.IsDefined(typeof(TankType), tankType)) tankType = null;

            if (id == null) id = Guid.NewGuid();

            SqliteDataReader reader = _database.ExecuteReader(
                "select * from Tanks where Id = @Id",
                new List<SqliteParameter>() { new SqliteParameter("Id", id.ToString()) });

            if (reader.Read())
            {
                actionLogCallback("Tank is exists");

                if (tankType != (TankType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")) ||
                    levelSensor.ToString() != reader.GetValue(reader.GetOrdinal("LevelSensor")).ToString() ||
                    flowSensor.ToString() != reader.GetValue(reader.GetOrdinal("FlowSensor")).ToString() ||
                    valve.ToString() != reader.GetValue(reader.GetOrdinal("EmptyValve")).ToString() ||
                    ratio != (double)reader.GetValue(reader.GetOrdinal("PercentToUnitRatio")) ||
                    unitType != (UnitType)(int)(long)reader.GetValue(reader.GetOrdinal("PercentToUnitType")) ||
                    name != reader.GetValue(reader.GetOrdinal("Name")).ToString())
                {
                    actionLogCallback("Need to update tank.");

                    _database.ExecuteScalar<int>(
                            "UPDATE Tanks SET Name = @Name, Type = @Type, LevelSensor = @LevelSensor, FlowSensor = @FlowSensor, EmptyValve = @Valve, PercentToUnitRatio = @PercentToUnitRatio, PercentToUnitType = @PercentToUnitType WHERE Id = @Id",
                            new List<SqliteParameter>() {
                                    new SqliteParameter("Id", id.ToString()),
                                    new SqliteParameter("Name", String.IsNullOrEmpty(name) ? reader.GetValue(reader.GetOrdinal("Name")).ToString() : name),
                                    new SqliteParameter("Type", tankType.GetDBChangeValue(reader.GetValue(reader.GetOrdinal("Type")))),
                                    new SqliteParameter("LevelSensor", levelSensor.GetDBChangeValue(reader.GetValue(reader.GetOrdinal("LevelSensor")))),
                                    new SqliteParameter("FlowSensor", flowSensor.GetDBChangeValue(reader.GetValue(reader.GetOrdinal("FlowSensor")))),
                                    new SqliteParameter("Valve", valve.GetDBChangeValue(reader.GetValue(reader.GetOrdinal("EmptyValve")))),
                                    new SqliteParameter("PercentToUnitRatio", ratio > 0 ? ratio : reader.GetValue(reader.GetOrdinal("PercentToUnitRatio"))),
                                    new SqliteParameter("PercentToUnitType", unitType.GetDBChangeValue(reader.GetValue(reader.GetOrdinal("PercentToUnitType")))),
                            });
                    actionLogCallback("Tank is updated.");
                }
            }
            else
            {
                actionLogCallback("Tank is NOT exists.");
                _database.ExecuteScalar<int>(
                "INSERT INTO Tanks (Id, Name, Type, LevelSensor, FlowSensor, EmptyValve, PercentToUnitRatio, PercentToUnitType) VALUES ( @Id, @Name, @Type, @LevelSensor, @FlowSensor, @Valve, @PercentToUnitRatio, @PercentToUnitType)",
                new List<SqliteParameter>() {
                                new SqliteParameter("Id", id.ToString()),
                                new SqliteParameter("Name", String.IsNullOrEmpty(name) ? "-" :  name),
                                new SqliteParameter("Type", tankType),
                                new SqliteParameter("LevelSensor", levelSensor.ToDBString() ),
                                new SqliteParameter("FlowSensor", flowSensor.ToDBString() ),
                                new SqliteParameter("Valve", valve.ToDBString()),
                                new SqliteParameter("PercentToUnitRatio", ratio),
                                new SqliteParameter("PercentToUnitType", unitType),
                    });
                actionLogCallback("Tank is created.");
            }
        }

        private void _saveGadget(Guid? id, GadgetType? type, string name, string port, Action<string> actionLogCallback)
        {
            if ((!String.IsNullOrEmpty(port) && type != null))
            {
                if (id == null) id = Guid.NewGuid();

                SqliteDataReader reader = _database.ExecuteReader(
                    "select * from Gadgets where Id = @Id",
                    new List<SqliteParameter>() { new SqliteParameter("Id", id.ToString()) });

                if (reader.Read())
                {
                    actionLogCallback("Gadget is exists");

                    if (type != (GadgetType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")) ||
                        port != reader.GetValue(reader.GetOrdinal("ConnectionPort")).ToString() ||
                        name != reader.GetValue(reader.GetOrdinal("Name")).ToString())
                    {
                        actionLogCallback("Need to update sensor.");
                        _database.ExecuteScalar<int>(
                            "UPDATE Gadgets SET Name = @Name, Type = @Type, ConnectionPort = @ConnectionPort, Status = 1, Value = 0 WHERE Id = @Id",
                            new List<SqliteParameter>() {
                                    new SqliteParameter("Id", id.ToString()),
                                    new SqliteParameter("Name", String.IsNullOrEmpty(name) ?"-" : name),
                                    new SqliteParameter("Type", type),
                                    new SqliteParameter("ConnectionPort", port),
                    });
                        actionLogCallback("Sensor is updated.");
                    }

                }
                else
                {
                    actionLogCallback("Sensor is NOT exists.");
                    _database.ExecuteScalar<int>(
                    "INSERT INTO Gadgets (ID, Name, Type, ConnectionPort, Value, Status) VALUES ( @Id, @Name, @Type, @ConnectionPort, 0, 1 )",
                    new List<SqliteParameter>() {
                            new SqliteParameter("Id", id.ToString()),
                            new SqliteParameter("Name", String.IsNullOrEmpty(name) ?"-" : name),
                            new SqliteParameter("Type", type),
                            new SqliteParameter("ConnectionPort", port),
                        });
                    actionLogCallback("Sensor is created.");
                }
            }
            else
            {
                if ((port == null && type == null)) actionLogCallback("Sensor info is not suplied.");
                if ((port != null && type == null)) actionLogCallback("Sensor info is ignored. Type is not suplied");
                if ((port == null && type != null)) actionLogCallback("Sensor info is ignored. Port is not suplied");
            }
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