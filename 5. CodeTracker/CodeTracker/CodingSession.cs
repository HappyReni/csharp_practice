﻿using System.Globalization;

namespace CodeTracker
{
    internal class CodingSession
    {
        public CodingSession() { }
        public CodingSession(DateTime start, DateTime end) 
        {
            Random rand = new();
            Id = rand.Next(90000);
            StartTime = start;
            EndTime = end;
            Duration = CalculateDuration();
            GetYears();
            GetWeeks();
        }
        public CodingSession(List<object> data)
        {
            Id = (int)data[0];
            StartTime = (DateTime)data[1];
            EndTime = (DateTime)data[2];
            Duration = (double)data[3];
            GetYears();
            GetWeeks();
        }

        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        public Dictionary<int, double> YearDuration { get; set; } = new();
        public Dictionary<string, double> WeekDuration { get; set; } = new();
        public List<string> Weeks { get; set; } = new();

        private double CalculateDuration() => (EndTime - StartTime).TotalHours;
        public List<object> GetField() => new List<object> { Id, StartTime, EndTime, Duration };
        private void GetYears()
        {
            var syear = StartTime.Year;
            var eyear = EndTime.Year;

            if (syear == eyear)
            {
                YearDuration[syear] = Duration;
            }
            else if(eyear-syear == 1)
            {
                var newyear = new DateTime(eyear, 01, 01);
                YearDuration[syear] = (newyear - StartTime).TotalHours;
                YearDuration[eyear] = (EndTime - newyear).TotalHours;
            }
            else 
            {
                var newyear = new DateTime(syear + 1, 01, 01);
                var newyear2 = new DateTime(eyear, 01, 01);
                YearDuration[syear] = (newyear - StartTime).TotalHours;
                for(int i=newyear.Year; i<=newyear2.Year-1; i++) 
                { 
                    YearDuration[syear+i] = 8760; 
                }
                YearDuration[eyear] = (EndTime-newyear2).TotalHours;
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
                int week = calendar.GetWeekOfYear(current, weekRule, firstDayOfWeek)-1;
                var day = calendar.GetDayOfWeek(current);

                string week_str = week < 10 ? $"0{week}" : week.ToString();

                if(day == DayOfWeek.Sunday)
                {
                    WeekDuration[year + "-" + week_str] = 168;
                    current = current.AddDays(7);
                }
                else if(current == StartTime)
                {
                    int move = (int)(7-day);
                    WeekDuration[year + "-" + week_str] = (new DateTime(StartTime.Year, StartTime.Month, StartTime.Day+move, 0, 0, 0) - current).TotalHours;
                    current = current.AddDays(move);
                }
                else
                {
                    WeekDuration[year + "-" + week_str] = (EndTime - current).TotalHours;
                    current = current.AddDays(7);
                }
                Weeks.Add(year + "-" + week_str);
            }
        }
    }
}
