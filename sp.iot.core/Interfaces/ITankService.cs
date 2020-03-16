using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface ITankService
    {
        List<Tank> Get(TankType? type);

        
        Tank Get(Guid tankId);

        SaveResponse<Tank> Save(TankSaveRequest request);

        
    }
}