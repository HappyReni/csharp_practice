using ConsoleTableExt;
using SportsResultNotifier;
using System.Data;

static void MakeTable(DataTable data, string tableName)
{
    ConsoleTableBuilder
        .From(data)
        .WithFormat(ConsoleTableBuilderFormat.Alternative)
        .WithTitle(tableName,ConsoleColor.Green)
        .ExportAndWriteLine();
    Console.WriteLine("".PadRight(24, '='));
}


Scrapper scrapper = new Scrapper();
var easternTable = scrapper.BuildTable("E");
var westernTable = scrapper.BuildTable("W");

MakeTable(easternTable, "Eastern Conference");
MakeTable(westernTable, "Western Conference");
//*[@id="confs_standings_W"]/thead/tr/th[1]
//string url = "https://www.basketball-reference.com/boxscores/";
//HtmlWeb web = new HtmlWeb();
//var Document = web.Load(url);
//var rows = Document.DocumentNode.SelectNodes("//*[@id=\"confs_standings_E\"]/tbody/tr");

//foreach (var row in rows)
//{
//    var cells = row.ChildNodes;
//    foreach (var cell in cells)
//    {
//        Console.WriteLine(cell.InnerText);
//    }
//    Console.WriteLine("==============");
//}
//*[@id="confs_standings_E"]/tbody/tr[1]
//*[@id="confs_standings_E"]/tbody/tr[1]/th/a
//*[@id="confs_standings_E"]/tbody/tr[2]
//*[@id="confs_standings_E"]/thead/tr/th[1]