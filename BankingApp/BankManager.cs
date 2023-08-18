using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    internal class BankManager
    {
        public BankManager()
        {
            _serviceSelector = "";
            MainMenu();
        }

        private string _serviceSelector { get; set; }
        private List<BankAccount> _accounts = new List<BankAccount>();

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("".PadLeft(24,'='));
            Console.WriteLine("Choose the service.");
            Console.WriteLine("1. Create New Account");
            Console.WriteLine("2. Check the balance"); 
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. View all accounts");

            Console.Write(">> ");
            _serviceSelector = Console.ReadLine();
            BeginService(_serviceSelector);
        }

        private void BeginService(string service)
        {
            Console.Clear();
            Console.WriteLine("".PadLeft(24, '='));

            switch (service)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    Console.WriteLine("2. 잔액 조회");
                    break;
                case "3":
                    Console.WriteLine("3. 계좌 이체");
                    break;
                case "4":
                    ViewAllAccounts();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    Console.ReadLine();
                    MainMenu();
                    break;
            }
        }

        private void CreateAccount()
        {
            Console.WriteLine("1. Create New Account\n\n");
            Console.WriteLine("Please type your name.");
            Console.Write(">> ");
            var name = Console.ReadLine();
            Console.WriteLine("Please input the amount of money to deposit.");
            Console.Write(">> ");
            var res = int.TryParse(Console.ReadLine(), out int money);

            if (res)
            {
                var acc = new BankAccount(name, money);
                _accounts.Add(acc);
            }
            else { Console.WriteLine("Please input an integer value only for the initial balance."); }
            Console.ReadLine();
            MainMenu();
        }

        private void ViewAllAccounts()
        {
            Console.WriteLine("4. Create New Account\n\n");
            Console.WriteLine($"{"Number",-10}{"name",-10}{"Balance",-10}");
            Console.WriteLine("".PadLeft(24, '='));

            foreach (var acc in _accounts)
            {
                acc.DisplayInfo();
            }
            Console.ReadLine();
            MainMenu();
        }
    }
}
