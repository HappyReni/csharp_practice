using ConsoleTableExt;
using System.Data.SqlTypes;
using System.Globalization;

namespace CodeTracker
{
    public class Manager
    {
        private SELECTOR Selector { get; set; }
        private SQLite SQL { get; set; }
        private List<CodingSession> SessionData { get; set; } = new();
        private Filter Filter { get; set; }
        private UI UI { get; set; }
        public Manager()
        {
            SQL = new();
            UI = new UI();
            SessionData = SQL.GetSQLData();
            Filter = new(SessionData);
            Selector = UI.MainMenu();
            while (true)
            {
                Action();
            }
        }
        private void Action()
        {
            switch (Selector)
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
                case SELECTOR.DROP:
                    Drop();
                    break;
                case SELECTOR.VIEW:
                    ViewTable();
                    break;
                case SELECTOR.REPORT:
                    var param = UI.FilterMenu();
                    var sessionList = new List<List<object>>();
                    Filter.SetParameters(param);
                    var period = "";

                    if ((FILTER_SELECTOR)param[0] == FILTER_SELECTOR.YEAR)
                    {
                        sessionList = Filter.FilterByYear();
                        period = "Years";
                    }
                    else if ((FILTER_SELECTOR)param[0] == FILTER_SELECTOR.WEEK)
                    {
                        sessionList = Filter.FilterByWeek();
                        period = "Weeks";
                    }
                    else if ((FILTER_SELECTOR)param[0] == FILTER_SELECTOR.DAY)
                    {
                        sessionList = Filter.FilterByDays();
                        period = "Days";
                    }
                    else break;
                    UI.MakeTable(sessionList,period);
                    //Report();
                    break;
                case SELECTOR.EXIT:
                    Environment.Exit(0);
                    break;
                default:
                    UI.Write("Invalid Input");
                    break;
            }
            Selector = UI.GoToMainMenu("Type any keys to continue.");
        }
        private void Drop()
        {
            ViewTable();
            SQL.DropTable();
            SessionData.Clear();
            SQL.CreateTable();
            UI.GoToMainMenu();
        }
        private void Insert()
        {
            ViewTable();
            try
            {
                UI.Write("Track coding time.");
                UI.Write("The time input format should be like this : (yyyy-MM-dd HH:mm:ss)");

                var input = UI.GetInput("Input start time first.").str;

                if (input == "r")
                {
                    DemoInsert();
                }
                else
                {
                    var start = Validation.ValidDateFormat(input);
                    var end = Validation.ValidDateFormat(UI.GetInput("Input end time.").str);

                    var code = new CodingSession(start, end);
                    SessionData.Add(code);
                    SQL.Insert(code);
                }

                ViewTable();
            }
            catch
            {
                UI.Write("Invalid Input. Try again.");
            }
        }

        private void DemoInsert()
        {
            Random rand = new();
            for (int i = 0; i < 10; i++)
            {
                var start_year = rand.Next(1, 9999);
                var end_year = rand.Next(start_year, 9999);
                var month = rand.Next(1, 13);
                var day = rand.Next(1, 31);
                var hour = rand.Next(0, 13);
                var min = rand.Next(1, 60);
                var sec = rand.Next(1, 60);
                var start = new DateTime(start_year, month, day, hour, min, sec);
                var end = new DateTime(end_year, month, day, hour, min, sec);
                var code_debug = new CodingSession(start, end);
                SessionData.Add(code_debug);
                SQL.Insert(code_debug);
            }
        }

        private void Delete()
        {
            ViewTable();
            try
            {
                var input = UI.GetInput("Select the ID of log to delete.");
                SQL.Delete(input.val);
                for (int i = 0; i < SessionData.Count; i++)
                {
                    if ((int)SessionData[i].Id == input.val)
                    {
                        SessionData.RemoveAt(i);
                        break;
                    }
                }
            }
            catch
            {
                UI.Write("Invalid Input. Try again.");
            }
        }
        private void Update()
        {
            ViewTable();
            try
            {
                var id = UI.GetInput("Select the ID of the log to update").val;
                UI.Write("The time input format should be like this : (yyyy-MM-dd HH:mm:ss)");
                var start = Validation.ValidDateFormat(UI.GetInput("Input start time first.").str);
                var end = Validation.ValidDateFormat(UI.GetInput("Input end time.").str);

                for (int i = 0; i < SessionData.Count; i++)
                {
                    if ((int)SessionData[i].Id == id)
                    {
                        var code = new CodingSession(start, end);
                        code.Id = id;
                        SessionData[i] = code;
                        SQL.Update(code);
                        break;
                    }
                }
            }
            catch
            {
                UI.Write("Invalid Input. Try again.");
            }
        }
        private void ViewTable()
        {
            List<List<object>> sessionList =
                SessionData.Select(session => session.GetField()).ToList();

            Console.Clear();
            ConsoleTableBuilder
                .From(sessionList)
                .WithTitle("Logs", ConsoleColor.Green)
                .WithColumn("ID", "Start Time", "End Time", "Duration(Hours)")
                .ExportAndWriteLine();
            Console.WriteLine("".PadRight(24, '='));
        }

        private void Report()
        {
            Console.Clear();
            Console.WriteLine("Report");
            Console.WriteLine("".PadRight(24, '='));
            Console.WriteLine("1. Yearly");
            Console.WriteLine("2. Weekly");
            Console.WriteLine("0. Main Menu\n");
            var select = UI.GetInput("Select ").val;

            switch (select)
            {
                case 1:
                    ReportYearlySession();
                    break;
                case 2:
                    ReportWeeklySession();
                    break;
                case 0:
                    UI.GoToMainMenu("Type any keys to continue.");
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }

        private void ReportYearlySession()
        {
            var input = UI.GetInput("Input the year").val;
            double duration = 0;
            var count = 0;

            foreach (var session in SessionData)
            {
                if (session.YearDuration.ContainsKey(input))
                {
                    duration += session.YearDuration[input];
                    count++;
                }
            }
            UI.GoToMainMenu($"{input} => total sessions : {count} total duration : {duration} average time : {duration / count}");
        }

        private void ReportWeeklySession()
        {
            var input = UI.GetInput("Input the week").str;
            double duration = 0;
            var count = 0;
            foreach (var session in SessionData)
            {
                if (session.WeekDuration.ContainsKey(input))
                {
                    duration += session.WeekDuration[input];
                    count++;
                }
            }
            UI.GoToMainMenu($"{input} => total sessions : {count} total duration : {duration} average time : {duration / count}");
        }
    }
}
