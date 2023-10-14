using ExerciseUI.Model;

namespace ExerciseUI.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        bool Create(T entity);
        bool Delete(int id);
        bool Update(T entity);
    }
    public class ExerciseRepository<T> : IRepository<T> where T : class
    {
        private readonly ExerciseContext context;

        public ExerciseRepository()
        {
            context = new();
        }
        
        public bool Create(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Error occured while adding an exercise.");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var item = context.Set<T>().Find(id);
                context.Set<T>().Remove(item);
                context.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Error occured while deleting an exercise.");
            }
        }

        public T Get(int id)
        {
            try
            {
                return context.Set<T>().Find(id);
            }
            catch
            {
                throw new Exception("Error occured while fetching an exercise.");
            }
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

        public bool Update(T entity)
        {
            try
            {
                context.Set<T>().Update(entity);
                context.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Error occured while updating an exercise.");
            }
        }
    }
}
