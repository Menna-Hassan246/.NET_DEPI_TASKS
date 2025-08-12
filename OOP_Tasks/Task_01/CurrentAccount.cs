using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class CurrentAccount : BankAccount
    {

        public decimal OverdraftLimit { get; set; }

        public CurrentAccount() : base()
        {
            OverdraftLimit = 0;
        }

        public CurrentAccount(int acc, string fullname, string national_ID, string phonenumber, string address, int balance, decimal overdraftLimit)
            : base(acc, fullname, national_ID, phonenumber, address, balance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override decimal CalculateInterest()
        {
            return 0; 
        }

        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"Overdraft Limit: {OverdraftLimit}");
        }
    }
}