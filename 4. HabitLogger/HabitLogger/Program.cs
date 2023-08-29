using HabitLogger;
using Microsoft.Data.Sqlite;
using System.Xml.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        new Manager();
    }
}

public class Manager
{
    private Dictionary<string,Habit> Habits { get; set; }
    private SELECTOR Selector { get; set; }
    private SQLite SQL { get; set; }
    public Manager()
    {
        Habits = new();
        SQL = new();
        MainMenu();
    }

    private void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("Habit Logger");
        Console.WriteLine("".PadRight(24, '='));
        Console.WriteLine("1. Register your habit.");
        Console.WriteLine("2. Insert a log");
        Console.WriteLine("3. Delete a log");
        Console.WriteLine("4. Update a log");
        Console.WriteLine("5. Drop a habit");
        Console.WriteLine("6. View habits");
        Console.WriteLine("0. Exit\n");
        Selector = (SELECTOR)GetInput("Select ").val;
        Action(Selector);
        Console.Clear();
    }
    private void Action(SELECTOR selector)
    {
        switch (selector)
        {
            case SELECTOR.REGISTER:
                Register();
                break;
            case SELECTOR.INSERT:
                Insert();
                break;
            case SELECTOR.DELETE:
                Delete();
                break;
            case SELECTOR.UPDATE:
                Update();
                break;
            case SELECTOR.VIEW:
                ViewTheHabits();
                break;
            case SELECTOR.DROP:
                Drop();
                break;
            case SELECTOR.EXIT:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }

    private void Register()
    {
        Console.Clear();
        var name = GetInput("Input the name of the habit.").str;
        var habit = new Habit(name);

        Habits.Add(name, habit);
        SQL.CreateTable($"\"{name}\"");
        WaitForInput("Register Completed.");
        MainMenu();
    }
    private void Insert()
    {
        Console.Clear();
        SQL.ViewTables();
        Console.WriteLine("".PadRight(24, '='));

        var table = GetInput("Input the name of the habit to insert a log.").str;
        var log = GetInput("Write the log.").str;

        Habits[table].InsertLog(log);
        SQL.Insert($"\"{table}\"", $"\"{log}\"");

        WaitForInput("Type any keys to continue.");
        MainMenu();
    }
    private void Delete()
    {

    }

    private void Drop()
    {
        Console.Clear();
        SQL.ViewTables();
        Console.WriteLine("".PadRight(24, '='));

        var table = GetInput("Input the name of the table to drop.").str;
        Habits.Remove(table);
        SQL.DropTable($"\"{table}\"");
        WaitForInput();
        MainMenu();
    }

    private void Update()
    {

    }
    private void ViewTheHabits()
    {
        Console.Clear();
        SQL.ViewTables();
        Console.WriteLine("".PadRight(24, '='));

        WaitForInput("Type any keys to continue.");
        MainMenu();
    }

    private (bool res, string str, int val) GetInput(string message)
    {
        // This function returns string input too in case you need it
        int number;
        Console.WriteLine(message);
        Console.Write(">> ");
        string str = Console.ReadLine();
        var res = int.TryParse(str, out number);

        number = res ? number : (int)SELECTOR.INVALID_SELECT;
        str = str == null ? "" : str;

        return (res, str, number);
    }
    private void WaitForInput(string message = "")
    {
        Console.WriteLine(message);
        Console.ReadLine();
    }

}