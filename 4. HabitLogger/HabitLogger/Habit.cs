namespace HabitLogger
{
    internal class Habit
    {
        public Habit(string name)
        {
            Name = name;
            Logs = new();
        }

        public string Name { get; set; }
        public List<string> Logs { get; set; }

        public void InsertLog(string log)
        {

        }

    }
}
