using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Session
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Score { get; set; }
        public int QuestionCount { get; set; }

        public Session(DateTime startTime, DateTime endTime, int score, int questionCount)
        {
            StartTime = startTime;
            EndTime = endTime;
            Score = score;
            QuestionCount = questionCount;
        }
    }
}
