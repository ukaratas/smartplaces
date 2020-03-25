using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IGadgetService
    {
        Gadget Get(Guid Id);

        Gadget BindGadgetData(SqliteDataReader reader);

        IEnumerable<Gadget> GetFiltered(Guid region, Guid section, GadgetTypeGroup? typeGroup, GadgetType? type);

        SaveResponse SetValue(Guid id, GadgetSetValueRequest value);
    }
}