using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankManagement
{
    public abstract class Account
    {
        private decimal _balance;
        private List<Transaction> _transactions;

        public Guid AccountNumber { get; private set; }
        public decimal CurrentBalance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        public DateTime DateOpened { get; private set; }
        public List<Transaction> Transactions
        {
            get { return _transactions; }
        }

        public Account()
        {
            AccountNumber = Guid.NewGuid();
            DateOpened = DateTime.Now;
            CurrentBalance = 0;
            _transactions = new List<Transaction>();
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return false;
            }
            CurrentBalance += amount;
            AddTransaction("Deposit", amount);
            return true;
        }

        public virtual bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return false;
            }
            if (amount <= CurrentBalance)
            {
                CurrentBalance -= amount;
                AddTransaction("Withdraw", amount);
                return true;
            }
            Console.WriteLine("Insufficient balance.");
            return false;
        }

        public bool Transfer(Account targetAccount, decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Transfer amount must be positive.");
                return false;
            }
            if (targetAccount == null)
            {
                Console.WriteLine("Target account cannot be null.");
                return false;
            }
            if (CurrentBalance >= amount)
            {
                CurrentBalance -= amount;
                targetAccount.CurrentBalance += amount;
                AddTransaction("Transfer", amount, targetAccount.AccountNumber);
                targetAccount.AddTransaction("TransferReceived", amount, this.AccountNumber);
                return true;
            }
            Console.WriteLine("Insufficient balance for transfer.");
            return false;
        }

        public void AddTransaction(string type, decimal amount, Guid? targetAccountNumber = null)
        {
            _transactions.Add(new Transaction(type, amount, targetAccountNumber));
        }

        public void DisplayTransactionHistory()
        {
            Console.WriteLine($"Transaction History for Account {AccountNumber}:");
            foreach (var t in _transactions)
            {
                string target = t.TargetAccountNumber != null ? $" to/from {t.TargetAccountNumber}" : "";
                Console.WriteLine($"{t.Date}: {t.Type} of {t.Amount:C}{target}");
            }
        }
    }
}

