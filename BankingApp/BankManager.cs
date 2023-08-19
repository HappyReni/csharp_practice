using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankingApp
{
    internal class BankManager
    {
        public BankManager()
        {
            _service = "";
            MainMenu();
        }

        private string _service { get; set; }
        private List<BankAccount> _accounts = new();

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("".PadLeft(24,'='));
            Console.WriteLine("Choose the service.");
            Console.WriteLine("1. Create New Account");
            Console.WriteLine("2. Check Balance"); 
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. View All Accounts");

            Console.Write(">> ");
            _service = Console.ReadLine();
            BeginService(_service);
        }


        private void GoBack(string s)
        {
            Console.ReadLine();
            if (s=="Main") MainMenu();
            else CheckBalance();

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
                    CheckBalance();
                    break;
                case "3":
                    Transfer();
                    break;
                case "4":
                    ViewAllAccounts();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    GoBack("Main");
                    break;
            }
        }

        private void CreateAccount()
        {
            Console.WriteLine("1. Create New Account\n\n");
            var input_name = GetInput("Please type your name.");
            var input_money = GetInput("Please input the amount of money to deposit.");

            if (input_money.res)
            {
                var acc = new BankAccount(input_name.str, input_money.val);
                _accounts.Add(acc);
            }
            else 
            { 
                Console.WriteLine("Please input an integer value only for the initial balance."); 
            }

            GoBack("Main");
        }

        private void CheckBalance()
        {
            Console.Clear();
            Console.WriteLine("2. Check Balance\n\n");
            var input_number = GetInput("Please type your account number. Press \"Exit\" if you want to go back to the main menu");

            if (input_number.res)
            {
                var acc = SearchAccount(input_number.val);

                if (acc != null) acc.CheckBalance();
                else
                {
                    Console.WriteLine("Invalid Account Number. Please Try Again.");
                    GoBack("Balance");
                }
            }
            else
            {
                if (input_number.str == "Exit") GoBack("Main");
                Console.WriteLine("Invalid Input. Please Try Again.");
                GoBack("Balance");
            }
            GoBack("Main");
        }

        private void Transfer()
        {
            Console.Clear();
            Console.WriteLine("3. Transfer\n\n");

            var input_number = GetInput("Please type your account number.");

            if (input_number.res)
            {
                var input_money = GetInput("Type the amount of money you would like to transfer.");
                var transfer_number = GetInput("Please type the account number that you would transfer.");

                var acc = SearchAccount(input_number.val);
                var acc_transfer = SearchAccount(transfer_number.val);

                if (acc != null && acc_transfer != null)
                {
                    if(acc.Withdraw(input_money.val)) acc_transfer.MakeDeposit(input_money.val);
                }
                else
                {
                    Console.WriteLine("Invalid Account Number. Please Try Again.");
                }

            }
            else
            {
                Console.WriteLine("Invalid Account Number. Please Try Again.");
            }
            GoBack("Main");

        }

        private static (bool res, string str, int val) GetInput(string s)
        {

            int number;
            Console.WriteLine(s);
            Console.Write(">> ");
            string str = Console.ReadLine();
            var res = int.TryParse(str , out number);

            str = str == null ? "" : str;
            return (res, str, number);
        }

        private BankAccount SearchAccount(int n)
        {
            foreach(var acc in _accounts)
            {
                if(acc.Number == n)
                {
                    return acc;
                }
            }
            return null;
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
            GoBack("Main");
        }
    }
}
