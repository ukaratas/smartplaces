using System;
using Xunit;
using sp.iot.core;

namespace sp.iot.tests
{
    public class DatabaseTestOverTank
    {
        private readonly IDatabase  _database;

        public DatabaseTestOverTank(IDatabase database)
        {
            _database = database;
        }

        [Fact]
        public void Test1()
        {
            //_database.Close();
            

        }
    }
}
