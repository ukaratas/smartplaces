using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Section : BaseItem
    {
        public Section()
        {
            Gadgets = new List<Gadget>();
        }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gadgets")]
        public List<Gadget> Gadgets { get; set; }

        [JsonPropertyName("type")]
        public SectionType Type { get; set; }

        [JsonPropertyName("row")]
        public long Row { get; set; }

        [JsonPropertyName("column")]
        public long Column { get; set; }

    }
}
