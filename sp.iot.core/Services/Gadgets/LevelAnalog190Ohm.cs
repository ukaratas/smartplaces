using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace sp.iot.core
{
    public class LevelAnalog190Ohm : IGadget
    {
        private readonly IConfiguration _config;
        private readonly IDatabase _database;

        public LevelAnalog190Ohm(IConfiguration config, IDatabase database)
        {
            _config = config;
            _database = database;
        }

        public SaveResponse SetValue(Guid id, GadgetSetValueRequest value)
        {
            throw new NotImplementedException();
        }
    }
}