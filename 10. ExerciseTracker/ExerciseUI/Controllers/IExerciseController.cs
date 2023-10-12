namespace ExerciseUI.Controllers
{
    internal interface IExerciseController <T> where T : class
    {
        IEnumerable<T> GetExercises();
        void AddExercise(T exercise);
    }
}
