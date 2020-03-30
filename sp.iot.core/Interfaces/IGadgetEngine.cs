using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IGadgetEngine
    {
        void Execute(Guid id, GadgetSetValueRequest value, Action<string> logCallback);
    }

}