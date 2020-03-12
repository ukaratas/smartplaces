using System;

namespace sp.iot.server
{
    public class Tank
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TankType Type { get; set; }

        public double LevelAsPercentage { get; set; }

        public double LevelAsUnit { get; set; }

        public string Unit { get; set; }

        public bool IsLevelMonitored { get; set; }
        public bool IsConsumptionMonitored { get; set; }

        public bool IsFlowMonitored { get; set; }

        public bool CanEmpty { get; set; }

    }
}
