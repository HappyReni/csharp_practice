using Microsoft.Data.Sqlite;
namespace HabitLogger
{
    internal class Habit
    {
        public string Name { get; set; }
        public List<string> Logs { get; set; }
        public Habit(string name)
        {
            Name = name;
            Logs = new();
        }
        public void InsertLog(string log) => Logs.Add(log);
    }
}
