using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IDatabase
    {
        SqliteConnection GetConnection();
    }

}