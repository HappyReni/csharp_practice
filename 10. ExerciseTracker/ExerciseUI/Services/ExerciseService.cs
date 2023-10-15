using ExerciseUI.Model;
using ExerciseUI.Repositories;

namespace ExerciseUI.Services
{
    internal class ExerciseService : IExerciseService<ExerciseModel>
    {
        private readonly ExerciseRepository<ExerciseModel> repository;
        public ExerciseService() { repository = new(); }

        public IEnumerable<ExerciseModel> GetExercises() => repository.GetAll();
        public bool AddExercise(ExerciseModel entity) 
        { 
            // Validation
            return repository.Create(entity); 
        }

        public bool RemoveExercise(int id)
        {
            return repository.Delete(id);
        }

        public bool UpdatingExercise(ExerciseModel entity)
        {
            return repository.Update(entity);
        }

        public ExerciseModel GetExercise(int id)
        {
            return repository.Get(id);
        }
    }
}
