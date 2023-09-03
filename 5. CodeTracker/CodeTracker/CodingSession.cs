using System;
using System.Collections.Generic;
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
        }
        public CodingSession(List<object> data)
        {
            Random rand = new();
            Id = (int)data[0];
            StartTime = (DateTime)data[1];
            EndTime = (DateTime)data[2];
            Duration = (double)data[3];
        }
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }

        private double CalculateDuration() => (EndTime - StartTime).TotalSeconds;
        public void Update(DateTime start, DateTime end)
        {
            StartTime = start;
            EndTime = end;
            Duration = CalculateDuration();
        }
        public List<object> GetField() => new List<object> { Id, StartTime, EndTime, Duration };
    }
}
