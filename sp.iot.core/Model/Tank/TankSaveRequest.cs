using System;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class TankSaveRequest : BaseItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public TankType? Type { get; set; }

        [JsonPropertyName("unit")]
        public LiquidUnitType? Unit { get; set; }

        [JsonPropertyName("percentage-ratio")]
        public Double PercentageRatio { get; set; }

        [JsonPropertyName("level-sensor-type")]
        public GadgetType? LevelSensorType { get; set; }

        [JsonPropertyName("level-sensor-id")]
        public Guid? LevelSensorId { get; set; }

        [JsonPropertyName("level-sensor-port")]
        public String LevelSensorConnectionPort { get; set; }

        [JsonPropertyName("flow-sensor-type")]
        public GadgetType? FlowSensorType { get; set; }

        [JsonPropertyName("flow-sensor-id")]
        public Guid? FlowSensorId { get; set; }

        [JsonPropertyName("flow-sensor-port")]
        public String FlowSensorConnectionPort { get; set; }

        [JsonPropertyName("valve-type")]
        public GadgetType? ValveType { get; set; }

        [JsonPropertyName("valve-id")]
        public Guid? ValveId { get; set; }

        [JsonPropertyName("valve-port")]
        public String ValveConnectionPort { get; set; }
    }
}