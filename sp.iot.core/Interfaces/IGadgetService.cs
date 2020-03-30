using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IGadgetService
    {


        Gadget BindGadgetData(SqliteDataReader reader);

        IEnumerable<Gadget> GetFiltered(Guid region, Guid section, GadgetTypeGroup? typeGroup, GadgetType? type);

        Gadget Get(Guid Id, bool includeActions);

        IEnumerable<Gadget> GetBySection(Guid section, bool includeActions);

        SaveResponse SetValue(Guid id, GadgetSetValueRequest value);

        void ProcessGadget(Guid id, GadgetSetValueRequest value, Action<string> logCallback);
    }
}