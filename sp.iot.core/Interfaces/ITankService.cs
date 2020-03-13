using System.Collections.Generic;
using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface ITankService
    {
        List<Tank> GetTanks(TankType? type);
    }
}