using ConsoleTableExt;
using SportsResultNotifier;
using System.Data;
using System.Net.Mail;
using System.Net;

static void MakeTable(DataTable data, string tableName)
{
    ConsoleTableBuilder
        .From(data)
        .WithFormat(ConsoleTableBuilderFormat.Alternative)
        .WithTitle(tableName, ConsoleColor.Green)
        .ExportAndWriteLine();
    Console.WriteLine("".PadRight(24, '='));
}


Scrapper scrapper = new Scrapper();
var easternTable = scrapper.BuildTable("E");
var westernTable = scrapper.BuildTable("W");

MakeTable(easternTable, "Eastern Conference");
MakeTable(westernTable, "Western Conference");
