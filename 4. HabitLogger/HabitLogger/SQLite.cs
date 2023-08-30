using Microsoft.Data.Sqlite;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;

namespace HabitLogger
{
    internal class SQLite
    {
        public SQLite(){}

        private SqliteConnection GetConnection()
        {
            string connStr = @"Data Source=habit_tracker.db";
            using var conn = new SqliteConnection(connStr);
            return conn;
        }
        
        public void CreateTable(string table)
        {
            var conn = GetConnection();
            conn.Open();

            string createTableQuery = $"CREATE TABLE IF NOT EXISTS {table} (Id INTEGER PRIMARY KEY, Time TEXT, Log TEXT)";
            using var createTableCommand = new SqliteCommand(createTableQuery, conn);
            createTableCommand.ExecuteNonQuery();
        }

        public void Insert(string table, string time, string log)
        {
            var conn = GetConnection();
            conn.Open();

            string insertQuery = $"INSERT INTO {table} (Time, Log) VALUES (@time, @log)";
            using var insertCommand = new SqliteCommand(insertQuery, conn);
            insertCommand.Parameters.AddWithValue("@time", time);
            insertCommand.Parameters.AddWithValue("@log", log);
            var res = insertCommand.ExecuteNonQuery();
            var ret = res == 0 ? "Failed to log." : "Successfully logged.";
            Console.WriteLine(ret);
        }

        public void Delete(string table, int idx)
        {
            var conn = GetConnection();
            conn.Open();

            string deleteQuery = $"DELETE FROM {table} where Id = {idx}";
            using var deleteCommand = new SqliteCommand(deleteQuery, conn);
            var res = deleteCommand.ExecuteNonQuery();
            var ret = res == 0 ? "Failed to delete." : "Successfully deleted.";
            Console.WriteLine(ret);
        }

        public void DropTable(string table)
        {
            var conn = GetConnection();
            conn.Open();

            try
            {
                string dropTableQuery = $"DROP TABLE {table}";
                using var dropTableCommand = new SqliteCommand(dropTableQuery, conn);
                dropTableCommand.ExecuteNonQuery();
                Console.WriteLine("Successfully dropped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewTables()
        {
            var conn = GetConnection();
            conn.Open();

            string viewTableQuery = $"SELECT name FROM sqlite_master WHERE type='table'";
            using var viewTableCommand = new SqliteCommand(viewTableQuery, conn);
            using var tableReader = viewTableCommand.ExecuteReader();

            if( tableReader.HasRows != true )
            {
                Console.WriteLine($"There is no table!");
                return;
            }
            while (tableReader.Read())
            {
                string tableName = tableReader.GetString(0);
                Console.WriteLine($"Table Name: {tableName}");

                string selectQuery = $"SELECT * From \"{tableName}\"";
                using var selectCommand = new SqliteCommand(selectQuery, conn);
                using var dataReader = selectCommand.ExecuteReader();

                int idx = 0 ;
                while (dataReader.Read())
                {
                    string date = dataReader.GetString(1);
                    string log = dataReader.GetString(2);
                    Console.WriteLine($"\t>{idx}:\t{date}\t{log}");
                    idx++;
                }
            }
        }
        
        public Dictionary<string,Habit> ToDictionary()
        {
            var conn = GetConnection();
            conn.Open();

            Dictionary<string, Habit> dict = new();
            string viewTableQuery = $"SELECT name FROM sqlite_master WHERE type='table'";
            using var viewTableCommand = new SqliteCommand(viewTableQuery, conn);
            using var tableReader = viewTableCommand.ExecuteReader();

            while (tableReader.Read())
            {
                string tableName = tableReader.GetString(0);
                string selectQuery = $"SELECT * From \"{tableName}\"";
                using var selectCommand = new SqliteCommand(selectQuery, conn);
                using var dataReader = selectCommand.ExecuteReader();
                Habit habit = new(tableName);

                while (dataReader.Read())
                {
                    string date = dataReader.GetString(1);
                    string log = dataReader.GetString(2);
                    habit.InsertLog(date,log);
                }
                dict.Add(tableName, habit);
            }
            return dict;
        }
    }

}
