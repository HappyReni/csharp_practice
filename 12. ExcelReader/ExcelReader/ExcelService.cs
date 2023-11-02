using ExcelReader.Models;

namespace ExcelReader
{
    public class ExcelService
    {
        public ExcelContext _context;

        public ExcelService(ExcelContext context)
        {
            _context = context;
        }

        public void Create(List<ExcelModel> models)
        {
            _context.Excels.AddRange(models);
            _context.SaveChanges();
        }
    }
}
