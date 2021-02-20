using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IGadgetActionService
    {
        List<GadgetAction> GetByGadgetSource(Guid gadgetId);
        List<GadgetAction> GetByGadgetTarget(Guid gadgetId);

        GadgetAction BindGadgetActionData(SqliteDataReader reader);

    }
}