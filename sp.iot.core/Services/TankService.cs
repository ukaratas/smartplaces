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

            _database.SaveItem(
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

            _database.SaveItem(
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

            _database.SaveItem(
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
            
            _database.SaveItem(
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