// See https://aka.ms/new-console-template for more information
using BankingApp;

internal class Program
{
    private static void Main(string[] args)
    {
        BankAccount bank = BankAccount.CreateBankAccount(0001,"Natalia",2000);
        BankManager bankManager = new BankManager();
    }
}