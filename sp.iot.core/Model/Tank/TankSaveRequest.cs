using System;

namespace sp.iot.core
{
    public class TankSaveRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TankType? Type { get; set; }
        public LiquidUnitType? Unit { get; set; }
        public Double PercentageRatio { get; set; }
    
        public GadgetType? LevelSensorType { get; set; }
        public Guid? LevelSensorId { get; set; }
        public String LevelSensorConnectionPort { get; set; }
    
        public GadgetType? FlowSensorType { get; set; }
        public Guid? FlowSensorId { get; set; }
        public String FlowSensorConnectionPort { get; set; }
    
        public GadgetType? ValveType { get; set; }
        public Guid? ValveId { get; set; }
        public String ValveConnectionPort { get; set; }
    }
}