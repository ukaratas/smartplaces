using Microsoft.Data.Sqlite;


namespace sp.iot.core
{
    public interface IDatabase
    {
        SqliteConnection GetConnection();
        SqliteDataReader ExecuteReader(SqliteCommand command);
    }

}