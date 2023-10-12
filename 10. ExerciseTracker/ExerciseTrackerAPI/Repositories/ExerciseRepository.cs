using ExerciseTrackerAPI.Model;

namespace ExerciseTrackerAPI.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Delete(int id);
        void Update(T entity);
    }
    public class ExerciseRepository<T> : IRepository<T> where T : class
    {
        private readonly ExerciseContext context;

        public ExerciseRepository()
        {
        }
        
        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll() 

        {
            return Context.Set<T>();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
