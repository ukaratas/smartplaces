using System;

namespace sp.iot.core
{
    public class Tank : BaseItem
    {
        public string Name { get; set; }

        public TankType Type { get; set; }

        public double PercentToUnitRatio { get; set; }

        public double LevelAsPercentage { get; set; }

        public double LevelAsUnit { get {return Math.Round(LevelAsPercentage * PercentToUnitRatio,2); } }

        public LiquidUnitType Unit { get; set; }

        public bool IsLevelMonitored { get { return LevelSensorId != null; } }
        public Guid? LevelSensorId { get; set; }
        
        public bool IsFlowMonitored { get { return FlowSensorId != null; } }
        public Guid? FlowSensorId { get; set; }
        
        public bool CanEmpty  { get { return EmptyValveId != null; } }
        public Guid? EmptyValveId { get; set; }


    }
}
