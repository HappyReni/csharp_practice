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
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }

        private double CalculateDuration() => (EndTime - StartTime).TotalSeconds;
        public List<object> GetField() => new List<object> { Id, StartTime, EndTime, Duration };
    }
}
