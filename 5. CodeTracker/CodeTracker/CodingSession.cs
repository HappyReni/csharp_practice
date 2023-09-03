using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTracker
{
    internal class CodingSession
    {
        public CodingSession(DateTime start, DateTime end) 
        {
            Random rand = new();
            Id = rand.Next(90000);
            StartTime = start;
            EndTime = end;
            Duration = CalculateDuration();
            Init();
            GetYears();
            GetWeeks();
        }
        public CodingSession(List<object> data)
        {
            Id = (int)data[0];
            StartTime = (DateTime)data[1];
            EndTime = (DateTime)data[2];
            Duration = (double)data[3];
            Init();
            GetYears();
            GetWeeks();
        }
        public static HashSet<int>? Years;
        public static List<string>? Weeks;
        public static List<DayOfWeek>? Days;
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        public List<int> Year { get; set; } = new();
        public List<string> Week { get; set; } = new();
        public List<DayOfWeek> Day { get; set; } = new();

        private static void Init()
        {
            Years = new();
            Weeks = new();
            Days = new();
        }
        private double CalculateDuration() => (EndTime - StartTime).TotalHours;
        public void Update(DateTime start, DateTime end)
        {
            StartTime = start;
            EndTime = end;
            Duration = CalculateDuration();
        }
        public List<object> GetField() => new List<object> { Id, StartTime, EndTime, Duration };
        private void GetYears()
        {
            for (int i = StartTime.Year; i <= EndTime.Year; ++i) 
            { 
                Year.Add(i);
                Years.Add(i);
            }
        }
        private void GetWeeks()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            Calendar calendar = cultureInfo.Calendar;
            CalendarWeekRule weekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            var current = StartTime;
            while (current <= EndTime) 
            {
                int year = calendar.GetYear(current);
                int week = calendar.GetWeekOfYear(current, weekRule, firstDayOfWeek);
                Week.Add(year + "-" + week);
                Weeks.Add(year + "-" + week);
                current = current.AddDays(7);
            }
        }
    }
}
