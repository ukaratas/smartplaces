using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class GadgetDefinition : BaseItem
    {
        public string Name { get; set; }

        public GadgetType Type { get; set; }
        
        public UnitType Unit { get; set; }
        
        public string ReadScript { get; set; }
        
        public string WriteScript { get; set; }
    }
}
