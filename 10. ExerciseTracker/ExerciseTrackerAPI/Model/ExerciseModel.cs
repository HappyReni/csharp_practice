namespace ExerciseTrackerAPI.Model
{
    public class ExerciseModel
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public TimeSpan Duration => DateStart - DateEnd;
        public string Comments { get; set; }
    }
}
