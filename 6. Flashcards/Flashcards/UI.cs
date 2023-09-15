using ConsoleTableExt;

namespace Flashcards
{
    internal class UI
    {
        public UI() {}

        public SELECTOR MainMenu()
        {
            Console.Clear();
            Write("Flashcards");
            Write("".PadRight(24, '='));
            Write("1. Create a Stack");
            Write("2. Manage a Stack");
            Write("3. Study");
            Write("0. Exit\n");
            var selector = (SELECTOR)GetInput("Select ").val;

            return selector;
        }
        public string CreateTable()
        {
            Console.Clear();
            Write("Create a Table");
            Write("".PadRight(24, '='));
            var name = GetInput("Type a name of table.").str;
            return name;
        }
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
        public void MakeTable(List<List<object>> sessionList, string period)
        {
            Console.Clear();
            if (period == "Records")
            {
                ConsoleTableBuilder
                .From(sessionList)
                .WithTitle("Records", ConsoleColor.Green)
                .WithColumn("ID", "Start Time", "End Time", "Duration(Hours)")
                .ExportAndWriteLine();
                Write("".PadRight(24, '='));
                return;
            }
            ConsoleTableBuilder
                .From(sessionList)
                .WithTitle($"Filter by {period}", ConsoleColor.Green)
                .WithColumn($"{period}","ID", "Start Time", "End Time", "Duration(Hours)")
                .ExportAndWriteLine();
            Write("".PadRight(24, '='));

        }

        public SELECTOR GoToMainMenu(string message = "")
        {
            WaitForInput(message);
            return MainMenu();
        }
        public (bool res, string str, int val) GetInput(string message)
        {
            // This function returns string input too in case you need it
            int number;
            Write(message);
            Console.Write(">> ");
            string str = Console.ReadLine();
            var res = int.TryParse(str, out number);

            number = res ? number : (int)SELECTOR.INVALID_SELECT;
            str = str == null ? "" : str;

            return (res, str, number);
        }
        public void WaitForInput(string message = "")
        {
            Write(message);
            Console.ReadLine();
        }
    }
}
