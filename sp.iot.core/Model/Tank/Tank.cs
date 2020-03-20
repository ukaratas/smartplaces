using System;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Tank : BaseItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public TankType Type { get; set; }

        [JsonPropertyName("percent-to-unit-ratio")]
        public double PercentToUnitRatio { get; set; }

        [JsonPropertyName("level-as-percentage")]
        public double LevelAsPercentage { get; set; }

        [JsonPropertyName("level-as-unit")]
        public double LevelAsUnit { get { return Math.Round(LevelAsPercentage * PercentToUnitRatio, 2); } }

        [JsonPropertyName("unit")]
        public LiquidUnitType Unit { get; set; }

        [JsonPropertyName("is-level-monitored")]
        public bool IsLevelMonitored { get { return LevelSensorId != null; } }
        
        [JsonPropertyName("level-sensor-id")]
        public Guid? LevelSensorId { get; set; }

        [JsonPropertyName("is-flow-monitored")]
        public bool IsFlowMonitored { get { return FlowSensorId != null; } }

        [JsonPropertyName("flow-sensor-id")]
        public Guid? FlowSensorId { get; set; }

        [JsonPropertyName("can-empty")]
        public bool CanEmpty { get { return EmptyValveId != null; } }

        [JsonPropertyName("empty-valve-id")]
        public Guid? EmptyValveId { get; set; }


    }
}
