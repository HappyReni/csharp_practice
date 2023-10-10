using Microsoft.EntityFrameworkCore;

namespace ExerciseTrackerAPI.Model
{
    public class ExerciseContext : DbContext
    {
        public ExerciseContext(DbContextOptions options) : base(options)
        {
        }

        protected ExerciseContext()
        {
        }

        public DbSet<ExerciseModel> Exercises { get; set; }
    }
}
