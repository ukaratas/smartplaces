using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Settings 
    {
        public Settings()
        {
            Regions = new List<Region>();
        }

        [JsonPropertyName("regions")]
        public List<Region> Regions { get; set; }
    }
}
