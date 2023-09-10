using ConsoleTableExt;

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
            Write("Coding Tracker");
            Write("".PadRight(24, '='));
            Write("1. Insert a log");
            Write("2. Delete a log");
            Write("3. Update a log");
            Write("4. DROP");
            Write("5. View Logs");
            Write("6. Read Report");
            Write("0. Exit\n");
            var selector = (SELECTOR)GetInput("Select ").val;


            return selector;
        }
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
        public List<object>? FilterMenu()
        {
            Console.Clear();
            Write("Filter");
            Write("".PadRight(24, '='));
            Write("1. Years");
            Write("2. Weeks");
            Write("3. Days");
            Write("0. Main Menu\n");
            var select = (FILTER_SELECTOR)GetInput("Select ").val;
            Console.Clear();
            var order = GetInput("Select the order > 0:Ascending, 1:Descending").val;
            switch (select)
            {
                case FILTER_SELECTOR.YEAR:
                    var startYear = GetInput("Start Year :").val;
                    var endYear = GetInput("End Year :").val;
                    return new List<object>() { FILTER_SELECTOR.YEAR, startYear, endYear, order };
                case FILTER_SELECTOR.WEEK:
                    var startWeek = GetInput("Start Week :").str;
                    var endWeek = GetInput("End Week :").str;
                    return new List<object>() { FILTER_SELECTOR.WEEK, startWeek, endWeek, order };
                case FILTER_SELECTOR.DAY:
                    var startDate = GetInput("Start Date :").str;
                    var endDate = GetInput("End Date :").str;
                    return new List<object>() { FILTER_SELECTOR.DAY, startDate, endDate, order };
                default:
                    Write("Invalid Input");
                    return new List<object>() { FILTER_SELECTOR.INVALID_SELECT, null, null, order };
            }
        }
        public void MakeTable(List<List<object>> sessionList, string period)
        {
            ConsoleTableBuilder
                .From(sessionList)
                .WithTitle($"Filter by {period}", ConsoleColor.Green)
                .WithColumn($"{period}","ID", "Start Time", "End Time", "Duration(Hours)")
                .ExportAndWriteLine();
            
            Write("".PadRight(24, '='));
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
            Write(message);
            Console.Write(">> ");
            string str = Console.ReadLine();
            var res = int.TryParse(str, out number);

            number = res ? number : (int)SELECTOR.INVALID_SELECT;
            str = str == null ? "" : str;

            return (res, str, number);
        }
        public void WaitForInput(string message = "")
        {
            Write(message);
            Console.ReadLine();
        }
    }
}
