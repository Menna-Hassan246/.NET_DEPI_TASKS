using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
   
        public class Transaction
        {
            public string Type { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
            public Guid? TargetAccountNumber { get; set; }

            public Transaction(string type, decimal amount, Guid? targetAccountNumber = null)
            {
                if (string.IsNullOrEmpty(type))
                {
                    Console.WriteLine("Transaction type cannot be empty.");
                    Type = "Unknown";
                }
                else
                {
                    Type = type;
                }
                if (amount <= 0)
                {
                    Console.WriteLine("Transaction amount must be positive.");
                    Amount = 0;
                }
                else
                {
                    Amount = amount;
                }
                Date = DateTime.Now;
                TargetAccountNumber = targetAccountNumber;
            }
        }
    }
   


