using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class Gadget : BaseItem
    {
        public Gadget()
        {
            TargetActions = new List<GadgetAction>();
            SourceActions = new List<GadgetAction>();
        }

        public string Name { get; set; }

        public string Configuration { get; set; }
        
        public double Value { get; set; }

        public string ComplexValue { get; set; }

        public GadgetStatus Status { get; set; }

        public PositionType SectionPosition { get; set; }

        public Guid Definition { get; set; }

        public string ReadFrequency { get; set; }

        public List<GadgetAction> TargetActions { get; set; }
        public List<GadgetAction> SourceActions { get; set; }
    }
}
