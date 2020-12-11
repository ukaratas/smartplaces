using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Gadget : BaseItem
    {
        public Gadget()
        {
            Actions = new List<GadgetAction>();
        }

        public string Name { get; set; }

        public string Configuration { get; set; }
        
        public double Value { get; set; }

        public string ComplexValue { get; set; }

        public GadgetStatus Status { get; set; }

        public PositionType SectionPosition { get; set; }

        public Guid Definition { get; set; }

        [JsonPropertyName("actions")]
        public List<GadgetAction> Actions { get; set; }
    }
}
