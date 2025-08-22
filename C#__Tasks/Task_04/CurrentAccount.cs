using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
    public class CurrentAccount : Account
    {

        public decimal OverdraftLimit { get; set; }

        public CurrentAccount(decimal overdraftLimit)
        {
            OverdraftLimit = overdraftLimit >= 0 ? overdraftLimit : throw new ArgumentException("Overdraft limit cannot be negative.");
        }

        public override bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return false;
            }
            if (amount <= CurrentBalance + OverdraftLimit)
            {
                CurrentBalance -= amount;
                AddTransaction("Withdraw", amount);
                return true;
            }
            Console.WriteLine("Exceeds overdraft limit.");
            return false;
        }
    }
}

