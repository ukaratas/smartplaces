
using Microsoft.Extensions.DependencyInjection;

namespace sp.iot.core
{
    public static class ConstantStrings
    {
        public static class SqlQueries
        {
            public static class Tank
            {
                public static class Get
                {
                    public const string NoParam = "select tn.Id, tn.Name, tn.Type, tn.PercentToUnitRatio, tn.PercentToUnitType, ls.id as LevelSensor, ls.Value as LevelSensorValue, fs.id as FlowSensor, ev.id as EmptyValve from Tanks as tn left join Gadgets as ls on LevelSensor = ls.id left join Gadgets as fs on FlowSensor = fs.id left join Gadgets as ev on EmptyValve = ev.id";
                    public const string IdParam = NoParam + " where tn.Id = @Id";
                    public const string TypeParam = NoParam + " where tn.Type = @Type";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE Tanks SET Name = @Name, Type = @Type, LevelSensor = @LevelSensor, FlowSensor = @FlowSensor, EmptyValve = @EmptyValve, PercentToUnitRatio = @PercentToUnitRatio, PercentToUnitType = @PercentToUnitType WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Tanks (Id, Name, Type, LevelSensor, FlowSensor, EmptyValve, PercentToUnitRatio, PercentToUnitType) VALUES ( @Id, @Name, @Type, @LevelSensor, @FlowSensor, @EmptyValve, @PercentToUnitRatio, @PercentToUnitType)";
                }
            }

            public static class Gadget
            {
                public static class Get
                {
                    public const string IdParam = "select * from Gadgets where Id = @Id";
                }

                public static class Save
                {
                    public const string UpdateWithId = "UPDATE Gadgets SET Name = @Name, Type = @Type, ConnectionPort = @ConnectionPort, Status = @Status, Value = @Value WHERE Id = @Id";
                    public const string Insert = "INSERT INTO Gadgets (ID, Name, Type, ConnectionPort, Value, Status) VALUES ( @Id, @Name, @Type, @ConnectionPort, @Value, @Status )";
                }

            }


        }
    }
}