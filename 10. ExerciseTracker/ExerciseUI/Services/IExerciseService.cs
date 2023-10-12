namespace ExerciseUI.Services
{
    public interface IExerciseService<T> where T : class
    {
        public IEnumerable<T> GetExercises();
        public void AddExercise(T entity);
    }
}
