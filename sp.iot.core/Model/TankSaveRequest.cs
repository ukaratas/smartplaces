using System;

namespace sp.iot.core
{
    public class TankSaveRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TankType Type { get; set; }
        public UnitType Unit { get; set; }
        public LevelSensorType? LevelSensor { get; set; }
        public String LevelSensorConnectionPort { get; set; }
        public LiquidFlowSensorType? FlowSensor { get; set; }
        public String FlowSensorConnectionPort { get; set; }
        public ValveType? ValveSensor { get; set; }
        public String ValveConnectionPort { get; set; }
    }
}