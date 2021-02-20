using System;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class GadgetAction : BaseItem
    {
        public Guid SourceGadget { get; set; }
        
        public Guid TargetGadget { get; set; }

        public int Order { get; set; }

        public string CanExecute { get; set; }

        public string Execute { get; set; }

    }
}
