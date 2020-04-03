using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class RegionRow : BaseItem
    {

        [JsonPropertyName("no")]
        public long No { get; set; }

        [JsonPropertyName("percetage")]
        public long Percentage { get; set; }
     
    }
}
