using System;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Gadget : BaseItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public GadgetType Type { get; set; }

        [JsonPropertyName("port")]
        public string Port { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("value-unit")]
        public UnitType ValueUnit { get; set; }

        [JsonPropertyName("value-to-target-ratio")]
        public double ValueToTargetRatio { get; set; }

        [JsonPropertyName("value-to-target-unit")]
        public UnitType ValueToTargetUnit { get; set; }

        [JsonPropertyName("complex-value")]
        public string ComplexValue { get; set; }

        [JsonPropertyName("status")]
        public GadgetStatus Status { get; set; }


        [JsonPropertyName("position-in-section")]
        public PositionType SectionPosition { get; set; }

        [JsonPropertyName("attached-to")]
        public Guid AttachedTo { get; set; }
    }
}
