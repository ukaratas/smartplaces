using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IScriptingService
    {
        ScriptResult Execute(GadgetAction action, ScriptParameter param);
    }


    
}