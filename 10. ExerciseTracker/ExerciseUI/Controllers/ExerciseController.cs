using ExerciseUI.Model;
using ExerciseUI.Services;

namespace ExerciseUI.Controllers
{
    internal class ExerciseController : IExerciseController<ExerciseModel>
    {
        private readonly ExerciseService service;

        public ExerciseController()
        {
            service = new();
        }

        public void AddExercise(ExerciseModel exercise)
        {
            service.AddExercise(exercise);
        }

        public IEnumerable<ExerciseModel> GetExercises()
        {
            return service.GetExercises();
        }
    }
}
