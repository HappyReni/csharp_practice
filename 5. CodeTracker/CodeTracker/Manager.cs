﻿using ConsoleTableExt;
using System.Data.SqlTypes;
using System.Globalization;

namespace CodeTracker
{
    public class Manager
    {
        private SELECTOR Selector { get; set; }
        private SQLite SQL { get; set; }
        private List<CodingSession> SessionData { get; set; }
        public Manager()
        {
            SQL = new();
            SessionData = SQL.GetSQLData();
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
            Console.WriteLine("4. DROP");
            Console.WriteLine("5. View Logs");
            Console.WriteLine("6. Read Report");
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
                case SELECTOR.DROP:
                    Drop();
                    break;
                case SELECTOR.VIEW:
                    ViewTable();
                    GoToMainMenu("Type any keys to continue.");
                    break;
                case SELECTOR.REPORT:
                    Report();
                    break;
                case SELECTOR.EXIT:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }
        private void Drop()
        {
            ViewTable();
            SQL.DropTable();
            SessionData.Clear();
            SQL.CreateTable();
            GoToMainMenu();
        }
        private void Insert()
        {
            ViewTable();
            try
            {
                Console.WriteLine("Track coding time.");
                Console.WriteLine("The time input format should be like this : (yyyy-MM-dd HH:mm:ss)");

                var input = GetInput("Input start time first.").str;

                if (input == "r")
                {
                    DemoInsert();
                }
                else
                {
                    var start = Validation.ValidDateFormat(input);
                    var end = Validation.ValidDateFormat(GetInput("Input end time.").str);

                    var code = new CodingSession(start, end);
                    SessionData.Add(code);
                    SQL.Insert(code);
                }

                ViewTable();
            }
            catch
            {
                Console.WriteLine("Invalid Input. Try again.");
            }
            GoToMainMenu("Type any keys to continue.");
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
                var input = GetInput("Select the ID of log to delete.");
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
                Console.WriteLine("Invalid Input. Try again.");
            }

            GoToMainMenu("Type any keys to continue.");
        }
        private void Update()
        {
            ViewTable();
            try
            {
                var id = GetInput("Select the ID of the log to update").val;
                Console.WriteLine("The time input format should be like this : (yyyy-MM-dd HH:mm:ss)");
                var start = Validation.ValidDateFormat(GetInput("Input start time first.").str);
                var end = Validation.ValidDateFormat(GetInput("Input end time.").str);

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
                Console.WriteLine("Invalid Input. Try again.");
            }
            GoToMainMenu("Type any keys to continue.");
        }
        private void GoToMainMenu(string message = "")
        {
            WaitForInput(message);
            MainMenu();
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
            var select = GetInput("Select ").val;

            switch (select)
            {
                case 1:
                    ReportYearlySession();
                    break;
                case 2:
                    ReportWeeklySession();
                    break;
                case 0:
                    GoToMainMenu("Type any keys to continue.");
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }

        private void ReportYearlySession()
        {
            var input = GetInput("Input the year").val;
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
            GoToMainMenu($"{input} => total sessions : {count} total duration : {duration} average time : {duration / count}");
        }

        private void ReportWeeklySession()
        {
            var input = GetInput("Input the week").str;
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
            GoToMainMenu($"{input} => total sessions : {count} total duration : {duration} average time : {duration / count}");
        }
        
        private void FilterDate()
        {
            Console.Clear();
            Console.WriteLine("Filter");
            Console.WriteLine("".PadRight(24, '='));
            Console.WriteLine("1. Years");
            Console.WriteLine("2. Weeks");
            Console.WriteLine("3. Days");
            Console.WriteLine("0. Main Menu\n");
            var select = GetInput("Select ").val;

            switch (select)
            {
                case 1:
                    ReportYearlySession();
                    break;
                case 2:
                    ReportWeeklySession();
                    break;
                case 0:
                    GoToMainMenu("Type any keys to continue.");
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        }

        private void SelectOrder()
        {

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
