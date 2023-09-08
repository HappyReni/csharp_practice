using ConsoleTableExt;
using System;
using System.Globalization;

namespace CodeTracker
{
    internal class Filter
    {
        private List<CodingSession> SessionData { get; set; }
        public Filter(List<CodingSession> sessionData) 
        { 
            SessionData = sessionData;
        }

        private void FilterByYear(int startYear, int endYear, int order)
        {
            List<List<object>> sessionList = new();
            IOrderedEnumerable<CodingSession> sortedList;

            if (order == 0)
            {
                sortedList =
                    from session in SessionData
                    where session.StartTime.Year > startYear && session.EndTime.Year < endYear
                    orderby session.StartTime.Year ascending
                    select session;
            }
            else
            {
                sortedList =
                    from session in SessionData
                    where session.StartTime.Year > startYear && session.EndTime.Year < endYear
                    orderby session.StartTime.Year descending
                    select session;
            }
            foreach (var session in sortedList)
            {
                sessionList.Add(session.GetField());
            }
            Console.Clear();
            ConsoleTableBuilder
                .From(sessionList)
                .WithTitle("Filter by Years", ConsoleColor.Green)
                .WithColumn("ID", "Start Time", "End Time", "Duration(Hours)")
                .ExportAndWriteLine();
            Console.WriteLine("".PadRight(24, '='));
        }
        private void FilterByWeek(string startWeek, string endWeek, int order)
        {
            List<List<object>> sessionList = new();

            foreach (var session in SessionData)
            {
                sessionList.AddRange(DivideByWeek(session));
            }

            sessionList = CheckFiltedWeek(sessionList, startWeek, endWeek);
            ConsoleTableBuilder
                .From(sessionList)
                .WithTitle("Filter by Weeks", ConsoleColor.Green)
                .WithColumn("Weeks", "ID", "Start Time", "End Time", "Duration(Hours)")
                .ExportAndWriteLine();
            Console.WriteLine("".PadRight(24, '='));
        }

        private List<List<object>> CheckFiltedWeek(List<List<object>> sessions, string startWeek, string endWeek)
        {
            List<List<object>> ret = new();
            for (int i = 0; i < sessions.Count; i++)
            {
                List<object>? session = sessions[i];
                var week = (string)session[0];

                if (IsWeekValid(startWeek, endWeek, week))
                {
                    ret.Add(sessions[i]);
                }
            }
            return ret;
        }
        private bool IsWeekValid(string week1, string week2, string week3)
        {
            string[] w1 = week1.Split('-');
            string[] w2 = week2.Split('-');
            string[] w3 = week3.Split('-');

            var w1_year = Int32.Parse(w1[0]);
            var w1_week = Int32.Parse(w1[1]);
            var w2_year = Int32.Parse(w2[0]);
            var w2_week = Int32.Parse(w2[1]);
            var w3_year = Int32.Parse(w3[0]);
            var w3_week = Int32.Parse(w3[1]);

            if (w1_year > w3_year) return false;
            else if (w3_year > w2_year) return false;
            else if (w1_year == w3_year && w1_week > w3_week) return false;
            else if (w2_year == w3_year && w2_week < w3_week) return false;
            else return true;
        }

        private int GetWeekGap(string week1, string week2)
        {
            string[] w1 = week1.Split('-');
            string[] w2 = week2.Split('-');

            var w1_year = Int32.Parse(w1[0]);
            var w1_week = Int32.Parse(w1[1]);
            var w2_year = Int32.Parse(w1[0]);
            var w2_week = Int32.Parse(w1[1]);

            if (w1_year == w2_year) return w2_week - w1_week;
            else if (w1_year + 1 == w2_year) return (52 - w1_week) + w2_week;
            else return (52 - w1_week) + ((w2_year - 1) - (w1_year + 1)) + w2_week;
        }
        private List<List<object>> DivideByWeek(CodingSession session)
        {
            var StartTime = session.StartTime;
            var EndTime = session.EndTime;
            List<List<object>> sessionList = new();
            CultureInfo cultureInfo = new CultureInfo("en-US");
            Calendar calendar = cultureInfo.Calendar;
            CalendarWeekRule weekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            var current = StartTime;
            while (current < EndTime)
            {
                int year = calendar.GetYear(current);
                int week = calendar.GetWeekOfYear(current, weekRule, firstDayOfWeek) - 1;
                var day = calendar.GetDayOfWeek(current);
                var list = new List<object>() { $"{year}-{week}" };

                if (current == StartTime)
                {
                    int move = (int)(7 - day);
                    var endDate = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day + move, 0, 0, 0);
                    list.AddRange(new CodingSession(current, endDate).GetField());

                    current = endDate;
                }
                else if (DateTime.Compare(current.AddDays(7), EndTime) == 1)
                {
                    list.AddRange(new CodingSession(current, EndTime).GetField());
                    current = current.AddDays(7);
                }
                else
                {
                    list.AddRange(new CodingSession(current, current.AddDays(7)).GetField());
                    current = current.AddDays(7);
                }
                sessionList.Add(list);
            }
            return sessionList;
        }
    }
}
