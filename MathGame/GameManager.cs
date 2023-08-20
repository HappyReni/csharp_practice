using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    internal class GameManager
    {
        private const int INVALID_SELECT = -1;
        private const int ROUND_COUNT = 5;
        public GameManager()
        {
            _selector = INVALID_SELECT;
            _point = 0;
            MainMenu();
        }

        private int _selector { get; set; }
        private int _point { get; set; }

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
            _selector = GetInput("").val;
            BeginGames(_selector);
        }

        private void BeginGames(int s)
        {
            Console.Clear();
            for(int i=0; i< ROUND_COUNT; i++) 
            {
                Operation(s);
            }
            Console.WriteLine($"Your final score is {_point}.");
            Console.WriteLine($"Press any button to go back to the main menu.");
            Console.ReadLine();
            MainMenu();
        }

        private void Operation(int s)
        {
            Random rand = new();
            int x = rand.Next(20);
            int y = rand.Next(20);

            int answer = 0;

            switch (s)
            {
                case 1:
                    Console.WriteLine("Addition Game");
                    Console.WriteLine($"{x} + {y}");
                    answer = x + y;
                    break;
                case 2:
                    Console.WriteLine("Substraction Game");
                    Console.WriteLine($"{x} - {y}");
                    answer = x - y;
                    break;
                case 3:
                    Console.WriteLine("Multiplication Game");
                    Console.WriteLine($"{x} * {y}");
                    answer = x * y;
                    break;
                case 4:
                    var _division_numbers = GetDivisionNumbers(x, y);
                    var x_div = _division_numbers.x;
                    var y_div = _division_numbers.y;
                    Console.WriteLine("Division Game");
                    Console.WriteLine($"{x_div} / {y_div}");
                    answer = x_div / y_div;
                    break;
                case 9:
                    Console.WriteLine("Bye Bye");
                    Console.ReadLine();
                    Environment.Exit( answer );
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    Console.ReadLine();
                    MainMenu();
                    break;
            }
            var input = GetInput("").val;
            if (answer == input)
            {
                _point++;
                Console.WriteLine("Your answer was correct!");
                Console.ReadLine();
            }
            else 
            {
                Console.WriteLine("Your answer was wrong!");
                Console.ReadLine();
            }
            Console.Clear();
        }

        private (bool res, string str, int val) GetInput(string s)
        {

            int number;
            Console.WriteLine(s);
            Console.Write(">> ");
            string str = Console.ReadLine();
            var res = int.TryParse(str, out number);

            number = res ? number : INVALID_SELECT;
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
