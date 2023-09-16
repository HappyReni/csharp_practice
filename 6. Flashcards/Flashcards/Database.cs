using System.Data.SqlClient;

namespace Flashcards
{
    internal class Database
    {
        public bool isConnected = false;
        public string currentDirectory;
        public string dbName;
        public string connInfo;

        public Database() 
        {
            currentDirectory = Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, @"..\..\..\"));
            dbName = "MyDB.mdf";
            connInfo = $@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ""{currentDirectory}{dbName}""; Integrated Security = True; Connect Timeout = 10;";
            isConnected = Init();
        }
        public bool Init()
        {
            var res = Connect();

            CreateTable("Stack");
            CreateTable("Flashcards");
            return res;
        }
        public bool Connect()
        {
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateTable(string name)
        {
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();
                    string createTableQuery = $"CREATE TABLE {name} (" +
                        $"ID INT PRIMARY KEY IDENTITY (1,1)," +
                        $"Name NVARCHAR(20))";
                    
                    using (SqlCommand cmd = new SqlCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Insert(Stack stack)
        {
            var name = stack.Name;
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();
                    string insertQuery = $"INSERT INTO Stack (Name) VALUES ('{name}')";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
