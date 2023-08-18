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
        private int Number
        {
            get => number;

            set
            {
                Random rand = new();
                number = rand.Next(MAXIMUM_ACCOUNTS_NUMBER);
            }
        }
        private string Owner { get; set; }
        private decimal Balance { get; set; }
        private static List<int> _accountNumbers = new();
        private static List<Transaction> _transactions = new();
        #endregion

        private void AddNumbers(int number) => _accountNumbers.Add(number);
        private static bool CheckUniqueNumber(int number)
        {
            if (_accountNumbers.Contains(number)) return true;
            else return false;
        }
        public void MakeDeposit(decimal amount)
        {
            Balance += amount;
            Console.WriteLine($"{Number}-{Owner}\t입금 되었습니다.");
            AddTransactions(amount, $"입금 발생:\t잔액: {Balance}\t입금액: {amount}");
        }
        public void CheckBalance() => Console.WriteLine($"{Number}-{Owner}\t잔액은 {Balance}원 입니다.");
        public void Withdraw(decimal amount)
        {
            if (Balance < amount)
            {
                Console.WriteLine($"{Number}-{Owner}\t잔액 부족으로 출금 취소 되었습니다.");
                AddTransactions(amount, $"잔액 부족으로 출금 요청 취소:\t잔액: {Balance}\t출금요청액: {amount}");
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"{Number}-{Owner}\t출금 되었습니다.");

                AddTransactions(amount, $"출금 발생:\t잔액: {Balance}\t출금액: {amount}");
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
