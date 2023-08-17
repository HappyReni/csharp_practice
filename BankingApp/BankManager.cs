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
            InitDisplay();
        }

        private string _serviceSelector { get; set; }

        private void InitDisplay()
        {
            Console.WriteLine("===============================");
            Console.WriteLine("이용하실 은행 서비스를 선택하세요.");
            Console.WriteLine("1. 신규 계좌 생성");
            Console.WriteLine("2. 잔액 조회"); 
            Console.WriteLine("3. 계좌 이체");
            Console.Write(">> ");
            _serviceSelector = Console.ReadLine();
            BeginService(_serviceSelector);
        }

        private void BeginService(string service)
        {
            Console.Clear();
            Console.WriteLine("===============================");

            switch (service)
            {
                case "1":
                    Console.WriteLine("1. 신규 계좌 생성");
                    break;
                case "2":
                    Console.WriteLine("2. 잔액 조회");
                    break;
                case "3":
                    Console.WriteLine("3. 계좌 이체");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    Console.ReadLine();
                    InitDisplay();
                    break;
            }
        }
    }
}
