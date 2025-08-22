using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
    public class SavAccount:Account
    {
        public decimal InterestRate { get; set; }

        public SavAccount(decimal interestRate)
        {
            InterestRate = interestRate > 0 ? interestRate : throw new ArgumentException("Interest rate must be positive.");
        }

        public void ApplyMonthlyInterest()
        {
            decimal interest = CurrentBalance * (InterestRate / 12 / 100);
            CurrentBalance += interest;
            AddTransaction("Interest", interest);
            Console.WriteLine($"Interest of {interest:C} applied. New balance: {CurrentBalance:C}");
        }
    }
}
