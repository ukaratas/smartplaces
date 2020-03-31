using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class ScriptParameter
    {
        public double SourceOldValue;
        public double SourceNewValue;
        public string SourceOldComplexValue;
        public string SourceNewComplexValue;
        public double TargetOldValue;
        public string TargetOldComplexValue;
    }
}
