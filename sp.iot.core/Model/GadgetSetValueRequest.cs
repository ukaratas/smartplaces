using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class GadgetSetValueRequest
    {

        [JsonPropertyName("complex-value")]
        public string ComplexValue { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

    }
}
