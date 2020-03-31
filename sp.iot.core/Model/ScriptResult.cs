using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sp.iot.core
{
    public class ScriptResult
    {
        public bool CanExecute;
        public double TargetNewValue;
        public string TargetNewComplexValue;
    }
}
