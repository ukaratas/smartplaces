using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IGadgetActionService
    {
        List<GadgetAction> GetByGadget(Guid gadgetId);

        GadgetAction BindGadgetActionData(SqliteDataReader reader);

    }
}