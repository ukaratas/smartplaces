using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Section : BaseItem
    {
        public Section()
        {
            Gadgets = new List<Gadget>();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gadgets")]
        public List<Gadget> Gadgets { get; set; }

        [JsonPropertyName("right-section")]
        public Guid RightSection { get; set; }

        [JsonPropertyName("left-section")]
        public Guid LeftSection { get; set; }

        [JsonPropertyName("top-section")]
        public Guid TopSection { get; set; }

        [JsonPropertyName("bottom-section")]
        public Guid BottomSection { get; set; }
    }
}
