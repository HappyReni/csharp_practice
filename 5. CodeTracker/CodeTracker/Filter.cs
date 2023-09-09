using ConsoleTableExt;
using System;
using System.Globalization;

namespace CodeTracker
{
    internal class Filter
    {
        private List<CodingSession> SessionData { get; set; }
        private FILTER_SELECTOR Selector { get; set; }
        private int? order {  get; set; }
        private int? StartYear { get; set; }
        private int? EndYear { get; set; }
        private string? StartWeek { get; set; }
        private string? EndWeek { get; set; }
        public Filter(List<CodingSession> sessionData) 
        { 
            SessionData = sessionData;
        }
        public void SetParameters(List<object> param)
        {
            if (param == null) { return; }
            order = (int?)param[3];
            if ((FILTER_SELECTOR)param[0] == FILTER_SELECTOR.YEAR)
            {
                StartYear = (int)param[1];
                EndYear = (int)param[2];
                FilterByYear();
            }
            else if ((FILTER_SELECTOR)param[0] == FILTER_SELECTOR.WEEK)
            {
                StartWeek = (string)param[1];   
                EndWeek = (string)param[2];
                FilterByWeek();
            }
            
        }
        private void FilterByYear()
        {
            List<List<object>> sessionList = new();
            IOrderedEnumerable<CodingSession> sortedList;

            if (order == 0)
            {
                sortedList =
                    from session in SessionData
                    where session.StartTime.Year > StartYear && session.EndTime.Year < EndYear
                    orderby session.StartTime.Year ascending
                    select session;
            }
            else
            {
                sortedList =
                    from session in SessionData
                    where session.StartTime.Year > StartYear && session.EndTime.Year < EndYear
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
        private void FilterByWeek()
        {
            List<List<object>> sessionList = new();

            foreach (var session in SessionData)
            {
                sessionList.AddRange(DivideByWeek(session));
            }

            sessionList = CheckFilterdWeek(sessionList);
            ConsoleTableBuilder
                .From(sessionList)
                .WithTitle("Filter by Weeks", ConsoleColor.Green)
                .WithColumn("Weeks", "ID", "Start Time", "End Time", "Duration(Hours)")
                .ExportAndWriteLine();
            Console.WriteLine("".PadRight(24, '='));
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
        private List<List<object>> CheckFilterdWeek(List<List<object>> sessions)
        {
            List<List<object>> ret = new();
            for (int i = 0; i < sessions.Count; i++)
            {
                List<object>? session = sessions[i];
                var week = (string)session[0];

                if (IsWeekValid(StartWeek, EndWeek, week))
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
       
    }
}
