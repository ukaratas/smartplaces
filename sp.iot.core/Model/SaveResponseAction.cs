using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class SaveResponseAction
    {
        [JsonPropertyName("time-stamp")]
        public DateTime TimeStamp { get; set; }

        public String Action { get; set; }
    }
}
