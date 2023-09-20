using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

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
            connInfo = $@"Data Source = (localdb)\MSSQLLocalDB; AttachDbFilename=""{currentDirectory}{dbName}""; Integrated Security = True; Connect Timeout = 10;";
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
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public Dictionary<string,Stack>? GetStacksFromDatabase()
        {
            Dictionary<string, Stack> stacks = new Dictionary<string, Stack>();
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM Stack";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("Id");
                                string name = reader.GetString("Name");
                                Stack stack = new(id, name);
                                stacks[name] = stack;
                            }
                        }
                    }
                }

                return stacks;
            }
            catch
            {
                return null;
            }
        }

        public bool CreateTable(string name)
        {
            string createTableQuery = name == "Stack" ?
                        $"CREATE TABLE {name} (" +
                        $"ID INT PRIMARY KEY IDENTITY (1,1)," +
                        $"Name NVARCHAR(20))" :
                        $"CREATE TABLE {name} (" +
                        $"ID INT PRIMARY KEY," +
                        $"StackId INT FOREIGN KEY REFERENCES Stack(ID) ON DELETE CASCADE," +
                        $"Front NVARCHAR(20)," +
                        $"Back NVARCHAR(20))";
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();
                    
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
        public int Insert(string name)
        {
            var id = -1;
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
                    string selectQuery = $"SELECT * FROM Stack WHERE Name = '{name}'";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = reader.GetInt32(0);
                            }
                        }
                    }
                }
                return id;
            }
            catch
            {
                return id;
            }
        }
        public bool Insert(Flashcard card)
        {
            var Id = card.Id;
            var stackId = card.StackId;
            var Front = card.Front;
            var Back = card.Back;
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();

                    string insertQuery = $"INSERT INTO dbo.Flashcards (Id,StackId,Front,Back) VALUES ({Id}, {stackId}, '{Front}','{Back}')";
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

        public bool Update(Flashcard card)
        {
            var id = card.Id;
            var back = card.Back;
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();

                    string updateQuery = $"UPDATE Flashcards SET Back='{back}' WHERE ID = {id}";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
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
        public bool Delete(string name)
        {
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();

                    string deleteQuery = $"DELETE FROM Stack WHERE Name = '{name}'";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
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
        public bool Delete(int idx)
        {
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();

                    string deleteeQuery = $"DELETE From Flashcards WHERE ID = {idx}";
                    using (SqlCommand cmd = new SqlCommand(deleteeQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    UpdateID();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateID()
        {
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();

                    string selectQuery = $"SELECT * FROM Flashcards";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                    {
                        List<(int,int)> tempIdx = new List<(int,int)> ();
                        using (SqlDataReader reader = cmd.ExecuteReader()) 
                        {
                            var new_idx = 1;
                            while(reader.Read())
                            {
                                var original_idx = reader.GetInt32(0);
                                var idx = (original_idx, new_idx);
                                tempIdx.Add(idx);
                                new_idx++;
                            }
                        }
                        foreach(var idx in tempIdx)
                        {
                            string updateQuery = $"UPDATE Flashcards SET ID={idx.Item2} WHERE ID = {idx.Item1}";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                            {
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                return true;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public List<Flashcard>? GetFlashcardsInStack(int stackId,string arg)
        {
            List<Flashcard> cards = new List<Flashcard>();

            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();

                    string selectQuery = $"SELECT * FROM Flashcards WHERE StackId = {stackId}";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string front = reader.GetString("Front");
                                string back = reader.GetString("Back");
                                var card = new Flashcard(stackId, front, back);
                                if (arg == "View") Flashcard.DownCount();
                                cards.Add(card);
                            }
                        }
                    }
                }
                return cards;
            }
            catch
            {
                return cards;
            }
        }

    }

}
