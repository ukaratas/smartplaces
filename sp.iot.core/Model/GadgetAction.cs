using System;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class GadgetAction : BaseItem
    {
        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("can-execute")]
        public string CanExecute { get; set; }

        [JsonPropertyName("target-value")]
        public string TargetValue { get; set; }

        [JsonPropertyName("target-complex-value")]
        public string TargetComplexValue { get; set; }

        [JsonPropertyName("target-gadget")]
        public Guid TargetGadget { get; set; }

    }
}
