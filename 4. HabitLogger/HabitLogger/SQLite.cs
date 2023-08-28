using Microsoft.Data.Sqlite;

namespace HabitLogger
{
    internal class SQLite
    {
        public SQLite()
        {
            ConnectDatabase();
        }

        private SqliteConnection GetConnection()
        {
            string connStr = @"Data Source=habit_tracker.db";
            using var conn = new SqliteConnection(connStr);
            return conn;
        }

        private void ConnectDatabase()
        {
            try
            {
                var conn = GetConnection();
                conn.Open();

                string select_version = "SELECT SQLITE_VERSION()";
                using SqliteCommand getSQLiteVersion = new SqliteCommand(select_version, conn);
                var version = getSQLiteVersion.ExecuteScalar().ToString();
                Console.WriteLine($"SQLite Version : {version}");
            }
            catch 
            {
                Console.WriteLine($"Failed to connect Database.");
            }
        }

        public void CreateTable(string table)
        {
            var conn = GetConnection();
            conn.Open();

            string createTableQuery = $"CREATE TABLE IF NOT EXISTS {table} (Id INTEGER PRIMARY KEY, Habit TEXT, Log TEXT)";
            using var createTableCommand = new SqliteCommand(createTableQuery, conn);
            createTableCommand.ExecuteNonQuery();
        }

        public void ViewTables()
        {
            var conn = GetConnection();
            conn.Open();

            string viewTableQuery = $"SELECT name FROM sqlite_master WHERE type='table'";
            using var viewTableCommand = new SqliteCommand(viewTableQuery, conn);
            using var tableReader = viewTableCommand.ExecuteReader();

            while(tableReader.Read())
            {
                string tableName = tableReader.GetString(0);
                Console.WriteLine($"Table Name: {tableName}");
            }
        }
    }

}
