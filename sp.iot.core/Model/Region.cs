using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Region : BaseItem
    {

        public Region()
        {
            Sections = new List<Section>();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public RegionType Type { get; set; }

        [JsonPropertyName("sections")]
        public List<Section> Sections { get; set; }
    }
}
