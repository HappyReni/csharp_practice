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
        public List<Stack>? GetStacksFromDatabase()
        {
            List<Stack> stacks = new List<Stack>();
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
                                stacks.Add(stack);
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
                        $"ID INT PRIMARY KEY IDENTITY (1,1)," +
                        $"StackId INT," +
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
        public bool Insert(Flashcard card)
        {
            var stackId = card.StackId;
            var Front = card.Front;
            var Back = card.Back;
            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();

                    string insertQuery = $"INSERT INTO Flashcards (StackId,Front,Back) VALUES ({stackId}, '{Front}','{Back}')";
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

        public List<Flashcard>? GetFlashcardsInStack(int stackId)
        {
            List<Flashcard> cards = new List<Flashcard>();

            try
            {
                using (SqlConnection conn = new(connInfo))
                {
                    conn.Open();

                    string selectQuery = $"SELECT * From Flashcards WHERE StackId = {stackId}";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string front = reader.GetString("Front");
                                string back = reader.GetString("Back");
                                var card = new Flashcard(stackId, front, back);
                                cards.Add(card);
                            }
                        }
                    }
                }
                return cards;
            }
            catch
            {
                return null;
            }
        }

    }

}
