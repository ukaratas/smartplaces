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
                ConstantStrings.SqlQueries.Tank.Get.IdParam,
                new List<SqliteParameter>() { new SqliteParameter("Id", tankId.ToString()) }
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
                reader = _database.ExecuteReader(ConstantStrings.SqlQueries.Tank.Get.NoParam);
            }
            else
            {
                reader = _database.ExecuteReader(
                    ConstantStrings.SqlQueries.Tank.Get.TypeParam,
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

            _saveItem(
                request.LevelSensorId,
                ConstantStrings.SqlQueries.Gadget.Get.IdParam,
                ConstantStrings.SqlQueries.Gadget.Save.Insert,
                ConstantStrings.SqlQueries.Gadget.Save.UpdateWithId,
                new List<SaveItemProperty> {
                    new SaveItemProperty { Name= "Name", Value = request.Name, IsRequired = true },
                    new SaveItemProperty { Name= "Type", Value = request.LevelSensorType, IsRequired = true },
                    new SaveItemProperty { Name= "ConnectionPort", Value = request.LevelSensorConnectionPort, IsRequired = false },
                    new SaveItemProperty { Name= "Value", Value = 0.0 },
                    new SaveItemProperty { Name= "Status", Value = GadgetStatus.Active},
                },
                (log) => { returnItem.AddAction("Level Sensor : " + log); }
            );

            _saveItem(
                request.FlowSensorId,
                ConstantStrings.SqlQueries.Gadget.Get.IdParam,
                ConstantStrings.SqlQueries.Gadget.Save.Insert,
                ConstantStrings.SqlQueries.Gadget.Save.UpdateWithId,
                new List<SaveItemProperty> {
                    new SaveItemProperty { Name= "Name", Value = request.Name, IsRequired = true },
                    new SaveItemProperty { Name= "Type", Value = request.FlowSensorType, IsRequired = true },
                    new SaveItemProperty { Name= "ConnectionPort", Value = request.FlowSensorConnectionPort, IsRequired = false },
                    new SaveItemProperty { Name= "Value", Value = 0.0 },
                    new SaveItemProperty { Name= "Status", Value = GadgetStatus.Active},
                },
                (log) => { returnItem.AddAction("Flow Sensor : " + log); }
            );

            _saveItem(
                request.ValveId,
                ConstantStrings.SqlQueries.Gadget.Get.IdParam,
                ConstantStrings.SqlQueries.Gadget.Save.Insert,
                ConstantStrings.SqlQueries.Gadget.Save.UpdateWithId,
                new List<SaveItemProperty> {
                    new SaveItemProperty { Name= "Name", Value = request.Name, IsRequired = true },
                    new SaveItemProperty { Name= "Type", Value = request.ValveType, IsRequired = true },
                    new SaveItemProperty { Name= "ConnectionPort", Value = request.ValveConnectionPort, IsRequired = false },
                    new SaveItemProperty { Name= "Value", Value = 0.0 },
                    new SaveItemProperty { Name= "Status", Value = GadgetStatus.Active},
                },
                (log) => { returnItem.AddAction("Valve : " + log); }
            );


            _saveItem(
                request.Id,
                ConstantStrings.SqlQueries.Tank.Get.IdParam,
                ConstantStrings.SqlQueries.Tank.Save.Insert,
                ConstantStrings.SqlQueries.Tank.Save.UpdateWithId,
                new List<SaveItemProperty> {
                    new SaveItemProperty { Name= "Name", Value = request.Name, IsRequired = true },
                    new SaveItemProperty { Name= "Type", Value = request.Type, IsRequired = true },
                    new SaveItemProperty { Name= "LevelSensor", Value = request.LevelSensorId },
                    new SaveItemProperty { Name= "FlowSensor", Value = request.FlowSensorId },
                    new SaveItemProperty { Name= "EmptyValve", Value = request.ValveId },
                    new SaveItemProperty { Name= "PercentToUnitRatio", Value = request.PercentageRatio },
                    new SaveItemProperty { Name= "PercentToUnitType", Value = request.Unit },
                },
                (log) => { returnItem.AddAction("Tank : " + log); }
            );

            _database.Close();
            return returnItem;
        }

        private void _saveItem(
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

            SqliteDataReader reader = _database.ExecuteReader(
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
                    _database.ExecuteScalar<int>(updateQuery, saveParameters);
                    logCallback("Item is updated.");
                }
            }
            else
            {
                logCallback("Items is NOT exists");
                _buildSaveParameters(properties, null, saveParameters, logCallback);
                _database.ExecuteScalar<int>(insertQuery, saveParameters);
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
                              if (oldValue != DBNull.Value && guidValue != Guid.Parse(oldValue.ToString()))
                              {
                                  hasChange = true;
                                  if (oldRecordReader != null) logCallback(string.Format("Property '{0}' is changed. Item will be updated.", item.Name));
                                  updateParameters.Add(new SqliteParameter(item.Name, guidValue));
                              }
                              else updateParameters.Add(new SqliteParameter(item.Name, oldValue));
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

        public class SaveItemProperty
        {
            public string Name { get; set; }

            public object Value { get; set; }

            public bool IsRequired { get; set; }
        }

/*
        private void _saveTank(Guid? id, TankType? tankType, string name, Guid? levelSensor, Guid? flowSensor, Guid? valve, double ratio, LiquidUnitType? unitType, Action<string> actionLogCallback)
        {

            if (unitType != null && !Enum.IsDefined(typeof(LiquidUnitType), unitType)) unitType = null;
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
                    unitType != (LiquidUnitType)(int)(long)reader.GetValue(reader.GetOrdinal("PercentToUnitType")) ||
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
                                    new SqliteParameter("Name", String.IsNullOrEmpty(name) ? reader.GetValue(reader.GetOrdinal("Name")).ToString() : name),
                                    new SqliteParameter("Type", type.GetDBChangeValue(reader.GetValue(reader.GetOrdinal("Type")))),
                                    new SqliteParameter("ConnectionPort", String.IsNullOrEmpty(port) ? reader.GetValue(reader.GetOrdinal("ConnectionPort")).ToString() : port),
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
*/
        private Tank _bindReaderData(SqliteDataReader reader)
        {
            Tank returnValue = new Tank
            {
                Id = reader.GetValueAsGuid("Id").Value,
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                Type = (TankType)(int)(long)reader.GetValue(reader.GetOrdinal("Type")),
                Unit = (LiquidUnitType)(int)(long)reader.GetValue(reader.GetOrdinal("PercentToUnitType")),
                LevelAsPercentage = reader.GetValueAsDouble("LevelSensorValue"),
                PercentToUnitRatio = reader.GetValueAsDouble("PercentToUnitRatio"),
                LevelSensorId = reader.GetValueAsGuid("LevelSensor"),
                FlowSensorId = reader.GetValueAsGuid("FlowSensor"),
                EmptyValveId = reader.GetValueAsGuid("FlowSensor"),
            };
            return returnValue;
        }

    }
}