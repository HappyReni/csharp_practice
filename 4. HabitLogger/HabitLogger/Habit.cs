using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;

namespace HabitLogger
{
    internal class Habit
    {
        public string Name { get; set; }
        public List<(string,string)> Logs { get; set; }
        public Habit(string name)
        {
            Name = name;
            Logs = new();
        }
        public void InsertLog(string time, string log) => Logs.Add((time, log));
        public void DeleteLog(int idx) => Logs.RemoveAt(idx);
    }
}
