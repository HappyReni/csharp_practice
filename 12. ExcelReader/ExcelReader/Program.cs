using OfficeOpenXml;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
using (var package = new ExcelPackage(new FileInfo("file.xlsx")))
{
    var worksheet = package.Workbook.Worksheets["Sheet1"];

    string valueA1 = worksheet.Cells["A1"].Value.ToString();
    Console.WriteLine(valueA1);
}
