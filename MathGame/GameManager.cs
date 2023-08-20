using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    internal class GameManager
    {
        private const int ROUND_COUNT = 5;
        public GameManager()
        {
            _selector = SELECTOR.INVALID_SELECT;
            _point = 0;
            _history = new();
            MainMenu();
        }

        private SELECTOR _selector { get; set; }
        private int _point { get; set; }
        private List<History> _history;

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("".PadRight(24, '='));
            Console.WriteLine("Choose your game.");
            Console.WriteLine("0. View Previous Games");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Substraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("9. Exit The Program");
            Console.WriteLine("".PadRight(24, '='));
            _selector = (SELECTOR)GetInput("").val;
            BeginGames(_selector);
        }

        private void BeginGames(SELECTOR s)
        {
            Console.Clear();
            for(int i=0; i< ROUND_COUNT; i++) 
            {
                Operation(s);
            }
            Console.WriteLine($"Your final score is {_point}.");
            WaitForInput($"Press any button to go back to the main menu.");
            MainMenu();
        }

        private void WaitForInput(string s) 
        {
            Console.WriteLine(s);
            Console.ReadLine();
        }

        private void Operation(SELECTOR s)
        {
            Random rand = new();
            int x = rand.Next(20);
            int y = rand.Next(20);

            int answer = 0;
            string question = "";

            switch (s)
            {
                case SELECTOR.ViewPreviousGames:
                    ShowHistory();
                    WaitForInput($"Press any button to go back to the main menu.");
                    MainMenu();
                    break;
                case SELECTOR.Addition:
                    Console.WriteLine("Addition Game");
                    question = $"{x} + {y}";
                    answer = x + y;
                    break;
                case SELECTOR.Substraction:
                    Console.WriteLine("Substraction Game");
                    question = $"{x} - {y}";
                    answer = x - y;
                    break;
                case SELECTOR.Multiplication:
                    Console.WriteLine("Multiplication Game");
                    question = $"{x} * {y}";
                    answer = x * y;
                    break;
                case SELECTOR.Division:
                    var _division_numbers = GetDivisionNumbers(x, y);
                    var x_div = _division_numbers.x;
                    var y_div = _division_numbers.y;

                    Console.WriteLine("Division Game");
                    question = $"{x_div} / {y_div}";
                    answer = x_div / y_div;
                    break;
                case SELECTOR.EXIT:
                    WaitForInput($"Bye Bye");
                    Environment.Exit(0);
                    break;
                default:
                    WaitForInput($"Invalid input. Try again.");
                    MainMenu();
                    break;
            }
            Console.WriteLine(question);
            var input = GetInput("").val;

            if (answer == input)
            {
                _point++;
                WaitForInput("Your answer was correct!");
                AddHistory(question, answer, input, "CORRECT");
            }
            else 
            {
                WaitForInput("Your answer was wrong!");
                AddHistory(question, answer, input, "WRONG");
            }
            Console.Clear();
        }
        private void AddHistory(string q, int ca, int a, string r)
        {
            DateTime dateTime = DateTime.Now;
            _history.Add(new History(dateTime, q, ca, a, r));
        }

        private void ShowHistory()
        {
            Console.WriteLine($"Time\t\t\t\tQuestion\tCorrectAnswer\tAnswer\t\tResult");
            Console.WriteLine("".PadRight(100, '='));

            foreach (var h in _history)
            {
                Console.WriteLine($"{h.Time}\t\t{h.Question}\t\t{h.CorrectAnswer}\t\t{h.Answer}\t\t{h.Result}");
            }
            Console.WriteLine("".PadRight(100, '='));
        }

        private (bool res, string str, int val) GetInput(string s)
        {

            int number;
            Console.WriteLine(s);
            Console.Write(">> ");
            string str = Console.ReadLine();
            var res = int.TryParse(str, out number);

            number = res ? number : (int)SELECTOR.INVALID_SELECT;
            str = str == null ? "" : str;

            return (res, str, number);
        }

        private (int x,int y) GetDivisionNumbers(int x, int y)
        {
            Random rand = new();

            while (x % y != 0)
            {
                x = rand.Next(1, 99);
                y = rand.Next(1, 99);
            }
            return (x, y);
        }
    }


}
