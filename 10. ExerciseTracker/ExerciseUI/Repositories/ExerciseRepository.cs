using ExerciseUI.Model;

namespace ExerciseUI.Repositories
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
            context = new();
        }
        
        public void Create(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Error occured while adding an exercise.");
            }
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
            try
            {
                return context.Set<T>();
            }
            catch
            {
                throw new Exception("Error occured while fetching exercises.");
            }
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
