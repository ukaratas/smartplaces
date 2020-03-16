using System;

namespace sp.iot.core
{
    public class Tank : BaseItem
    {
        public string Name { get; set; }

        public TankType Type { get; set; }

        public double LevelAsPercentage { get; set; }

        public double LevelAsUnit { get; set; }

        public UnitType Unit { get; set; }
        public bool IsLevelMonitored { get; set; }
        public LevelSensorType? LevelSensor { get; set; }
        public String LevelSensorConnectionPort { get; set; }
        public bool IsConsumptionMonitored { get; set; }
        public LiquidFlowSensorType? FlowSensor { get; set; }
        public String FlowSensorConnectionPort { get; set; }
        public bool IsFlowMonitored { get; set; }
        public bool CanEmpty { get; set; }
        public ValveType? ValveSensor { get; set; }
        public String ValveConnectionPort { get; set; }

    }
}
