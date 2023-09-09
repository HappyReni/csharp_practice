namespace CodeTracker
{
    internal class UI
    {
        public UI()
        {
        }

        public SELECTOR MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Coding Tracker");
            Console.WriteLine("".PadRight(24, '='));
            Console.WriteLine("1. Insert a log");
            Console.WriteLine("2. Delete a log");
            Console.WriteLine("3. Update a log");
            Console.WriteLine("4. DROP");
            Console.WriteLine("5. View Logs");
            Console.WriteLine("6. Read Report");
            Console.WriteLine("0. Exit\n");
            var selector = (SELECTOR)GetInput("Select ").val;


            return selector;
        }
        public List<object>? FilterMenu()
        {
            Console.Clear();
            Console.WriteLine("Filter");
            Console.WriteLine("".PadRight(24, '='));
            Console.WriteLine("1. Years");
            Console.WriteLine("2. Weeks");
            Console.WriteLine("3. Days");
            Console.WriteLine("0. Main Menu\n");
            var select = (FILTER_SELECTOR)GetInput("Select ").val;
            Console.Clear();
            var order = GetInput("Select the order > 0:Ascending, 1:Descending").val;

            switch (select)
            {
                case FILTER_SELECTOR.YEAR:
                    var startDate = GetInput("Start Year :").val;
                    var endDate = GetInput("End Year :").val;
                    return new List<object>() { FILTER_SELECTOR.YEAR, startDate, endDate, order };
                case FILTER_SELECTOR.WEEK:
                    var startWeek = GetInput("Start Week :").str;
                    var endWeek = GetInput("End Week :").str;
                    return new List<object>() { FILTER_SELECTOR.WEEK, startWeek, endWeek, order };
                case FILTER_SELECTOR.DAY:
                    GoToMainMenu("Type any keys to continue.");
                    return null;
                default:
                    Console.WriteLine("Invalid Input");
                    return null;
            }
        }

        public SELECTOR GoToMainMenu(string message = "")
        {
            WaitForInput(message);
            return MainMenu();
        }
        public (bool res, string str, int val) GetInput(string message)
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
        public void WaitForInput(string message = "")
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
