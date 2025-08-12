using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class SavingAccount:BankAccount
    {
        
            public decimal InterestRate { get; set; }

            public SavingAccount() : base()
            {
                InterestRate = 0;
            }

            public SavingAccount(int acc,string fullname, string national_ID, string phonenumber, string address, int balance, decimal interestRate)
                : base( acc,fullname, national_ID, phonenumber, address, balance)
            {
                InterestRate = interestRate;
            }

            public override decimal CalculateInterest()
            {
                return Balance * InterestRate / 100;
            }

            public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"Interest Rate: {InterestRate}%");
            
        }

    }
}
