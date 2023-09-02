using ConsoleTableExt;
using System.Data.SqlTypes;

namespace CodeTracker
{
    public class Manager
    {
        private SELECTOR Selector { get; set; }
        private SQLite SQL { get; set; }
        public Manager()
        {
            SQL = new();
            MainMenu();
        }

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Coding Tracker");
            Console.WriteLine("".PadRight(24, '='));
            Console.WriteLine("1. Insert a log");
            Console.WriteLine("2. Delete a log");
            Console.WriteLine("3. Update a log");
            Console.WriteLine("5. View Logs");
            Console.WriteLine("0. Exit\n");
            Selector = (SELECTOR)GetInput("Select ").val;
            Action(Selector);
        }
        private void Action(SELECTOR selector)
        {
            switch (selector)
            {
                case SELECTOR.INSERT:
                    Insert();
                    break;
                case SELECTOR.DELETE:
                    Delete();
                    break;
                case SELECTOR.UPDATE:
                    Update();
                    break;
                case SELECTOR.VIEW:
                    ViewTheHabits();
                    break;
                case SELECTOR.EXIT:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }
        private void Insert()
        {
            ViewTables();
            try
            {
                var tableData = new List<List<object>>
                {
                    new List<object>{ "Sakura Yamamoto", "Support Engineer", "London", 46},
                    new List<object>{ "Serge Baldwin", "Data Coordinator", "San Francisco", 28, "something else" },
                    new List<object>{ "Shad Decker", "Regional Director", "Edinburgh"},
                };
                ConsoleTableBuilder.From(tableData).ExportAndWriteLine();
                var log = GetInput("Write the log with start time and end time.").str;
                var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SQL.Insert($"\"{time}\"", $"\"{log}\"");
            }
            catch
            {
                Console.WriteLine("Invalid Input. Try again.");
            }
            GoToMainMenu("Type any keys to continue.");
        }
        private void Delete()
        {
            //ViewTables();
            try
            {
                var input = GetInput("Select the index of the log to delete");
                SQL.Delete(input.val);
            }
            catch
            {
                Console.WriteLine("Invalid Input. Try again.");
            }

            GoToMainMenu("Type any keys to continue.");
        }
        private void Update()
        {
            try
            {
                var idx = GetInput("Select the index of the log to update");
                var input = GetInput("Input the new log");
                var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                SQL.Update($"\"{time}\"", $"\"{input.str}\"", idx.val);
            }
            catch
            {
                Console.WriteLine("Invalid Input. Try again.");
            }

            GoToMainMenu("Type any keys to continue.");
        }
        private void ViewTheHabits()
        {
            ViewTables();
            GoToMainMenu("Type any keys to continue.");
        }

        private void GoToMainMenu(string message = "")
        {
            WaitForInput(message);
            MainMenu();
        }

        private void ViewTables()
        {
            Console.Clear();
            SQL.ViewTables();
            Console.WriteLine("".PadRight(24, '='));
        }

        private (bool res, string str, int val) GetInput(string message)
        {
            // This function returns string input too in case you need it
            int number;
            Console.WriteLine(message);
            Console.Write(">> ");
            string str = Console.ReadLine();
            var res = int.TryParse(str, out number);

            number = res ? number : (int)SELECTOR.INVALID_SELECT;
            str = str == null ? "" : str;

            return (res, str, number);
        }
        private void WaitForInput(string message = "")
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
