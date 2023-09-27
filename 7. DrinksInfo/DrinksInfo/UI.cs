﻿using ConsoleTableExt;

namespace DrinksInfo
{
    internal class UI
    {
        public UI() { }

        //public SELECTOR MainMenu()
        //{
        //    Console.Clear();
        //    Write("Flashcards");
        //    Write("".PadRight(24, '='));
        //    Write("1. Create a Stack");
        //    Write("2. Manage a Stack");
        //    Write("3. View all the study sessions.");
        //    Write("0. Exit\n");
        //    var selector = (SELECTOR)GetInput("Select ").val;

        //    return selector;
        //}
        //public string CreateStack()
        //{
        //    Console.Clear();
        //    Write("Create a stack");
        //    Write("".PadRight(24, '='));
        //    var name = GetInput("Type a name of stack.").str;
        //    return name;
        //}
        //public int ManageStack(string name)
        //{
        //    Console.Clear();
        //    Write($"Current Stack : {name}");
        //    Write("Choose the number of action you want.\n");
        //    Write("1. View all flashcards in stack.");
        //    Write("2. Put a new flashcard into stack.");
        //    Write("3. Edit a flashcard.");
        //    Write("4. Delete a flashcard.");
        //    Write("5. Study current stack.");
        //    Write("6. Change current stack.");
        //    Write("7. Delete current stack.");
        //    Write("0. Return to main menu\n");
        //    Write("".PadRight(24, '='));

        //    return GetInput("Select").val;
        //}
        public static void Write(string text)
        {
            Console.WriteLine(text);
        }
        public static void Write(int text)
        {
            Console.WriteLine(text);
        }
        public static void Clear()
        {
            // Somehow, Console.Clear() doesn't work properly. it just skips lines.
            // This code clears the console.
            // https://github.com/dotnet/runtime/issues/28355
            Console.Write("\f\u001bc\x1b[3J");
        }
        public static void MakeTable<T>(List<T> data, string tableName) where T : class
        {
            Clear();
            ConsoleTableBuilder
                .From(data)
                .WithTitle(tableName, ConsoleColor.Green)
                .WithColumn(tableName)
                .ExportAndWriteLine(TableAligntment.Center);
            Console.WriteLine("".PadRight(24, '='));
        }

        //public SELECTOR GoToMainMenu(string message = "")
        //{
        //    WaitForInput(message);
        //    return MainMenu();
        //}
        public (bool res, string str, int val) GetInput(string message)
        {
            // This function returns string input too in case you need it
            int number;
            Write(message);
            Console.Write(">> ");
            string str = Console.ReadLine();
            var res = int.TryParse(str, out number);

            number = res ? number : -1;
            str = str == null ? "" : str;

            return (res, str, number);
        }
        public void WaitForInput(string message = "")
        {
            Write(message);
            Console.ReadKey();
        }
    }
}