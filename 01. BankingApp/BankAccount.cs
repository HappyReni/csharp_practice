using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
    public class BankAccount
    {
        private const int MAXIMUM_ACCOUNTS_NUMBER = 10000;

        #region Constructor
        public BankAccount(string owner, int balance)
        {
            Number = 0;
            Owner = owner;
            Balance = Math.Max(balance, 0);

            if (CheckUniqueNumber(number))
            {
                throw new InvalidOperationException("Can't create an account due to a duplicate number.");
            }
            else
            {
                AddNumbers(Number);
            }

            Console.WriteLine($"Account {Number} was created for {Owner} with {Balance} initial balance.");
        }
        #endregion

        #region fields
        private int number;
        public int Number
        {
            get => number;

            set
            {
                Random rand = new();
                number = rand.Next(MAXIMUM_ACCOUNTS_NUMBER);
            }
        }
        private string Owner { get; set; }
        public int Balance { get; set; }
        private static List<int> _accountNumbers = new();
        private static List<Transaction> _transactions = new();
        #endregion

        private void AddNumbers(int number) => _accountNumbers.Add(number);
        private static bool CheckUniqueNumber(int number)
        {
            if (_accountNumbers.Contains(number)) return true;
            else return false;
        }
        public void MakeDeposit(int amount)
        {
            Balance += amount;
            Console.WriteLine($"{Number} - {Owner}\t{amount} has been deposited.");
            AddTransactions(amount, $"Deposit:\tBalance: {Balance}\tAmount: {amount}");
        }
        public void CheckBalance() => Console.WriteLine($"{Number,-10}{Owner}\tBalance : {Balance}");
        public bool Withdraw(int amount)
        {
            if (Balance < amount)
            {
                Console.WriteLine($"{Number} - {Owner}\tA request for withdraw has been cancelled due to a lack of balance.");
                AddTransactions(amount, $"A cancelled request with a lack of balance:\tBalance: {Balance}\tAmount: {amount}");
                return false;
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"{Number} - {Owner}\t{amount} has been withdrawn.");

                AddTransactions(amount, $"Withdraw:\tBalance: {Balance}\tAmount: {amount}");
                return true;
            }
        }
        private void AddTransactions(decimal amount, string note)
        {
            var time = DateTime.Now;
            note = Number +"\t"+ note;
            var tr = new Transaction(amount, note, time);
            _transactions.Add(tr);
        }

        public void showHistory()
        {
            foreach (var tr in _transactions)
            {
                Console.WriteLine($"{tr.time}\t{tr.note}\t{tr.amount}");
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"{Number,-10}{Owner,-10}{Balance,-10}");
        }
    }

    class Transaction
    {
        public Transaction(decimal amount, string note, DateTime time)
        {
            this.amount = amount;
            this.note = note;
            this.time = time;
        }

        public decimal amount { get; set; }
        public string note { get; set; }
        public DateTime time { get; set; }

    }
}
