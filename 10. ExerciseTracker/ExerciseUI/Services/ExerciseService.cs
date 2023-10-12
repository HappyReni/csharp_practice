using ExerciseUI.Model;
using ExerciseUI.Repositories;

namespace ExerciseUI.Services
{
    internal class ExerciseService : IExerciseService<ExerciseModel>
    {
        private readonly ExerciseRepository<ExerciseModel> repository;
        public ExerciseService() { repository = new(); }

        public IEnumerable<ExerciseModel> GetExercises() => repository.GetAll().ToList();
        public void AddExercise(ExerciseModel entity) 
        { 
            // Validation
            repository.Create(entity); 
        }
    }
}
