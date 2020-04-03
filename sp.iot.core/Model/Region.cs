using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Region : BaseItem
    {

        public Region()
        {
            Sections = new List<Section>();
            Rows = new List<RegionRow>();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public RegionType Type { get; set; }

        [JsonPropertyName("sections")]
        public List<Section> Sections { get; set; }

        [JsonPropertyName("rows")]
        public List<RegionRow> Rows { get; set; }
    }
}
