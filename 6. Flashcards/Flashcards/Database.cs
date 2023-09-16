using System.Data.SqlClient;

namespace CodeTracker
{
    internal class Database
    {
        public bool isConnected = false;
        public Database() 
        {
            isConnected = init();
        }

        public bool init() => Connect();
        public bool Connect()
        {
            var dbName = "MyDB.mdf";
            var currentDirectory = Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, @"..\..\..\"));

            string connInfo = $@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ""{currentDirectory}{dbName}""; Integrated Security = True; Connect Timeout = 10;";
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

        public void CreateTable()
        {

        }
    }

}
