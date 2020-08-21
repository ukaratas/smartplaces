using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;

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

        [JsonPropertyName("background-image")]
        public string BackgroundImage { get; set; }

        [JsonPropertyName("aspect-ratio")]
        public double AspectRatio { get; set; }

        [JsonPropertyName("type")]
        public RegionType Type { get; set; }

        [JsonPropertyName("sections")]
        public List<Section> Sections { get; set; }

        [JsonPropertyName("rows")]
        public List<RegionRow> Rows { get; set; }

        [JsonPropertyName("layout-rows-count")]
        public long RowsCount
        {
            get
            {
                if (Sections.Count > 0)
                    return Sections.Max(section => section.Row);
                else
                    return 0;
            }
        }

        [JsonPropertyName("layout-column-count")]
        public long ColumnCount
        {
            get
            {
                if (Sections.Count > 0)
                    return Sections.Max(section => section.Column);
                else
                    return 0;
               
            }
        }
    }
}
