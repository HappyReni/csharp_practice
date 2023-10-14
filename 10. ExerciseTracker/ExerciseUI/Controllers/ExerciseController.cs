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

        public bool AddExercise(ExerciseModel exercise)
        {
            return service.AddExercise(exercise);
        }

        public ExerciseModel GetExercise(int id)
        {
            return service.GetExercise(id);
        }

        public IEnumerable<ExerciseModel> GetExercises()
        {
            return service.GetExercises();
        }

        public bool RemoveExercise(int id)
        {
            return service.RemoveExercise(id);
        }

        public bool UpdateExercise(ExerciseModel entity)
        {
            return service.UpdatingExercise(entity);
        }
    }
}
