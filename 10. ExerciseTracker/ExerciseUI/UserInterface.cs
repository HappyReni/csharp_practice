using ConsoleTableExt;
using ExerciseUI.Controllers;
using ExerciseUI.Model;

namespace ExerciseUI
{
    internal class UserInterface
    {
        private SELECTOR Selector { get; set; }
        private ExerciseController controller { get; set; }
        public UserInterface() 
        { 
            Selector = MainMenu();
            controller = new ExerciseController();
            while (true)
            {
                Action();
            }
        }

        public static SELECTOR MainMenu()
        {
            Console.Clear();
            Write("Exercise Tracker");
            Write("".PadRight(24, '='));
            Write("1. Add a track");
            Write("2. View a track");
            Write("3. Update a track");
            Write("4. Delete a track");
            Write("5. View all track");
            Write("0. Exit\n");

            return (SELECTOR)GetInput("Select ").val;
        }
        private void Action()
        {
            switch (Selector)
            {
                case SELECTOR.CREATE:
                    CreateTrack();
                    break;
                case SELECTOR.READ:
                    //ReadShift();
                    break;
                case SELECTOR.UPDATE:
                    //UpdateShift();
                    break;
                case SELECTOR.DELETE:
                    //DeleteShift();
                    break;
                case SELECTOR.VIEWALL:
                    ViewAllTracks();
                    break;
                case SELECTOR.EXIT:
                    Environment.Exit(0);
                    break;
                default:
                    Write("Invalid Input");
                    break;
            }
            Selector = GoToMainMenu("Type any keys to continue.");
        }

        private void CreateTrack()
        {
            DateTime startTime = DateTime.Parse(GetInput("Input a start time.").str);
            DateTime endTime = DateTime.Parse(GetInput("Input an end time.").str);
            string comment = GetInput("Type a comment.").str;
            var exercise = new ExerciseModel() { DateStart = startTime, DateEnd = endTime, Comments = comment};

            controller.AddExercise(exercise);
            Write("Successfully Added.");
        }
        private void ViewAllTracks()
        {
            var exercises = controller.GetExercises().ToList();
            UserInterface.MakeTable(exercises, "All Tracks");
        }

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
                .ExportAndWriteLine();
            Console.WriteLine("".PadRight(24, '='));
        }

        public static (bool res, string str, int val) GetInput(string message)
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
        public static void WaitForInput(string message = "")
        {
            Write(message);
            Console.ReadKey();
        }
        public SELECTOR GoToMainMenu(string message = "")
        {
            WaitForInput(message);
            return MainMenu();
        }
    }
}
