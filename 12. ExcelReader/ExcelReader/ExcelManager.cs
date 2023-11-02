using ExcelReader.Models;
using OfficeOpenXml;

namespace ExcelReader
{
    public class ExcelManager
    {
        public ExcelManager() 
        { 
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Read();
        }
        public void Read()
        {
            using (var package = new ExcelPackage(new FileInfo("file.xlsx")))
            {
                var worksheet = package.Workbook.Worksheets["Sheet1"];
                var colCount = worksheet.Dimension.End.Column;
                var rowCount = worksheet.Dimension.End.Row;
                var cols = worksheet.Columns;
                for (int i = 2; i < rowCount+1; i++)
                {
                    for (int j =0; j < colCount; j++)
                    {
                        char col = (char)('A' + j);

                        Console.WriteLine(worksheet.Cells[$"{col}{i}"].Value.ToString());
                    }
                }
                //string valueA1 = worksheet.Cells["A1"].Value.ToString();
            }

        }
    }
}
