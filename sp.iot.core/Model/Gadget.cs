using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Gadget : BaseItem
    {
        public Gadget()
        {
            Actions = new List<GadgetAction>();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type-group")]
        public GadgetTypeGroup TypeGroup { get; set; }

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

        [JsonPropertyName("value-to-target")]
        public double ValueToTarget { get { return ValueToTargetRatio * Value; } }

        [JsonPropertyName("complex-value")]
        public string ComplexValue { get; set; }

        [JsonPropertyName("status")]
        public GadgetStatus Status { get; set; }

        [JsonPropertyName("position-in-section")]
        public PositionType SectionPosition { get; set; }

        [JsonPropertyName("attached-to")]
        public Guid AttachedTo { get; set; }

        [JsonPropertyName("actions")]
        public List<GadgetAction> Actions { get; set; }
    }
}
