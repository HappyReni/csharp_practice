using HabitLogger;
using Microsoft.Data.Sqlite;

internal class Program
{
    private static void Main(string[] args)
    {
        new Manager();
    }
}

public class Manager
{
    private List<Habit> Habits { get; set; }
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
        Console.WriteLine("5. View habbits");
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
        Habits.Add(new Habit(name));
        SQL.CreateTable($"\"{name}\"");
        WaitForInput("Register Completed.");
        MainMenu();
    }
    private void Insert()
    {

    }
    private void Delete()
    {

    }
    private void Update()
    {

    }
    private void ViewTheHabits()
    {
        Console.Clear();
        SQL.ViewTables();
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