using System;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class GadgetAction : BaseItem
    {
        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("when")]
        public double SourceValue { get; set; }

        [JsonPropertyName("set")]
        public double TargetValue { get; set; }

        [JsonPropertyName("target-gadget")]
        public Guid TargetGadget { get; set; }

        [JsonPropertyName("on-execute-script")]
        public string Script { get; set; }
    }
}
