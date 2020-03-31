using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis.Scripting;

namespace sp.iot.core
{
    public class ScriptRunner
    {
        public ScriptRunner<bool> CanExecuteRunner;
        public string CanExecuteScript;
        public ScriptRunner<double> TargetNewValueRunner;
        public string TargetNewValueScript;
        public ScriptRunner<string> TargetNewComplexValueRunner;
        public string TargetNewComplexValueScript;
    }
}
