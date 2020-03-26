using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IGadget
    {
        SaveResponse SetValue(Guid id, GadgetSetValueRequest value);
    }

}