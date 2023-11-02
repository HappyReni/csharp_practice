using ExcelReader.Models;
using OfficeOpenXml;

namespace ExcelReader
{
    public class ExcelManager
    {
        public ExcelService _service;
        private List<ExcelModel> _excelModels;
        public ExcelManager()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _service = new ExcelService(new ExcelContext());
            _excelModels = Read();
            Add();
        }
        public List<ExcelModel> Read()
        {
            using (var package = new ExcelPackage(new FileInfo("file.xlsx")))
            {
                var worksheet = package.Workbook.Worksheets["Sheet1"];
                var colCount = worksheet.Dimension.End.Column;
                var rowCount = worksheet.Dimension.End.Row;
                var res = new List<ExcelModel>();

                for (int i = 2; i < rowCount + 1; i++)
                {
                    //for (int j =0; j < colCount; j++)
                    //{
                    //    char col = (char)('A' + j);

                    //    Console.WriteLine(worksheet.Cells[$"{col}{i}"].Value.ToString());
                    //}
                    var name = worksheet.Cells[$"A{i}"].Value.ToString();
                    var age = Int32.Parse(worksheet.Cells[$"B{i}"].Value.ToString());
                    var job = worksheet.Cells[$"C{i}"].Value.ToString();
                    var address = worksheet.Cells[$"D{i}"].Value.ToString();
                    res.Add(new ExcelModel { Name = name, Age = age, Job = job, Address = address });
                }
                return res;
            }
        }
        public void Add()
        {
            _service.Create(_excelModels);
        }
    }
}
