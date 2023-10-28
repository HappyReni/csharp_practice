using ExcelReader.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelReader
{
    public class ExcelContext : DbContext
    {
        protected ExcelContext()
        {
        }

        public ExcelContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Excel;Integrated Security=true");
        }

        public DbSet<ExcelModel> Excels { get; set; } = null;
    }
}
