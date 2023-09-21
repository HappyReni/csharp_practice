using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Session
    {
        public int StackId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Score { get; set; }
        public int QuestionCount { get; set; }

        public Session(int stackId, DateTime startTime, DateTime endTime, int score, int questionCount)
        {
            StackId = stackId;
            StartTime = startTime;
            EndTime = endTime;
            Score = score;
            QuestionCount = questionCount;
        }

        public List<object> GetField()
        {
            return new List<object> { StackId, StartTime, EndTime, Score, QuestionCount };
        }
    }
}
