using Microsoft.Data.Sqlite;
using System.Configuration;
using System.Collections.Specialized;

namespace CodeTracker
{
    internal class SQLite
    {
        public SQLite() 
        {
            TableName = ConfigurationManager.AppSettings.Get("TableName");
            CreateTable();
        }
        public string TableName { get; set; }

        private SqliteConnection GetConnection()
        {
            string connStr = ConfigurationManager.AppSettings.Get("DatabasePath");
            using var conn = new SqliteConnection(connStr);
            return conn;
        }
        
        public void CreateTable()
        {
            var conn = GetConnection();
            conn.Open();

            string createTableQuery = $"CREATE TABLE IF NOT EXISTS {TableName} (Id INTEGER PRIMARY KEY, Start TEXT, End TEXT, Log TEXT)";
            using var createTableCommand = new SqliteCommand(createTableQuery, conn);
            createTableCommand.ExecuteNonQuery();
        }

        public void Insert(string time, string log)
        {
            var conn = GetConnection();
            conn.Open();

            string insertQuery = $"INSERT INTO {TableName} (Time, Log) VALUES (@time, @log)";
            using var insertCommand = new SqliteCommand(insertQuery, conn);
            insertCommand.Parameters.AddWithValue("@time", time);
            insertCommand.Parameters.AddWithValue("@log", log);
            var res = insertCommand.ExecuteNonQuery();
            var ret = res == 0 ? "Failed to log." : "Successfully logged.";
            Console.WriteLine(ret);
        }

        public void Delete(int idx)
        {
            var conn = GetConnection();
            conn.Open();

            string deleteQuery = $"DELETE FROM {TableName} WHERE Id = {idx}";
            using var deleteCommand = new SqliteCommand(deleteQuery, conn);
            var res = deleteCommand.ExecuteNonQuery();
            var ret = res == 0 ? "Failed to delete." : "Successfully deleted.";
            Console.WriteLine(ret);
        }

        public void Update(string time, string log, int idx)
        {
            var conn = GetConnection();
            conn.Open();

            string updateQuery = $"UPDATE {TableName} SET Time = @time, Log = @log WHERE Id = @idx";
            using var updateCommand = new SqliteCommand(updateQuery, conn);
            updateCommand.Parameters.AddWithValue("@time", time);
            updateCommand.Parameters.AddWithValue("@log", log);
            updateCommand.Parameters.AddWithValue("@idx", idx);

            var res = updateCommand.ExecuteNonQuery();
            var ret = res == 0 ? "Failed to update." : "Successfully updated.";
            Console.WriteLine(ret);
        }

        public void DropTable()
        {
            var conn = GetConnection();
            conn.Open();

            try
            {
                string dropTableQuery = $"DROP TABLE {TableName}";
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

            if ( tableReader.HasRows != true )
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
                    int id = dataReader.GetInt32(0);
                    string date = dataReader.GetString(1);
                    string log = dataReader.GetString(2);
                    Console.WriteLine($"\t>{id}:\t{date}\t{log}");
                    idx++;
                }
            }
        }
    }

}
