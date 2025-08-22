using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
    public class Customer
    {
        private string _cid;
        private string _fullname;
        private string _nationalID;
        private DateTime _birth;
        private List<Account> _accounts;

        public string CID
        {
            get { return _cid; }
            set { _cid = value; }
        }

        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }

        public string NationalID
        {
            get { return _nationalID; }
            set { _nationalID = value; }
        }

        public DateTime Birth
        {
            get { return _birth; }
            set { _birth = value; }
        }

        public List<Account> Accounts
        {
            get { return _accounts; }
        }

        public Customer(string fullname, string nationalID, DateTime birth)
        {
            CID = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(fullname))
            {
                Console.WriteLine("Full name cannot be empty.");
                Fullname = "Unknown";
            }
            else
            {
                Fullname = fullname;
            }
            if (string.IsNullOrEmpty(nationalID))
            {
                Console.WriteLine("National ID cannot be empty.");
                NationalID = "Unknown";
            }
            else
            {
                NationalID = nationalID;
            }
            if (birth > DateTime.Now.AddYears(-150) && birth <= DateTime.Now)
            {
                Birth = birth;
            }
            else
            {
                Console.WriteLine("Invalid birth date. Using default.");
                Birth = DateTime.Now.AddYears(-30);
            }
            _accounts = new List<Account>();
        }

        public void DisplayCustomer()
        {
            Console.WriteLine($"ID: {CID}\nNational ID: {NationalID}\nFull Name: {Fullname}\nBirth: {Birth.ToShortDateString()}\nTotal Balance: {TotalBalance():C}");
        }

        public bool Update(string name, DateTime birth)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return false;
            }
            if (birth <= DateTime.Now.AddYears(-150) || birth > DateTime.Now)
            {
                Console.WriteLine("Invalid birth date.");
                return false;
            }
            Fullname = name;
            Birth = birth;
            Console.WriteLine("Updated Info:");
            DisplayCustomer();
            return true;
        }

        public decimal TotalBalance()
        {
            decimal total = 0;
            for (int i = 0; i < _accounts.Count; i++)
            {
                total += _accounts[i].CurrentBalance;
            }
            return total;
        }

        public bool CanRemove()
        {
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (_accounts[i].CurrentBalance != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool AddAccount(Account account)
        {
            if (account == null)
            {
                Console.WriteLine("Account cannot be null.");
                return false;
            }
            _accounts.Add(account);
            return true;
        }

        public void DisplayAllTransactionHistory()
        {
            Console.WriteLine($"Transaction History for Customer {Fullname} (ID: {CID}):");
            if (_accounts.Count == 0)
            {
                Console.WriteLine("No accounts found.");
                return;
            }
            for (int i = 0; i < _accounts.Count; i++)
            {
                _accounts[i].DisplayTransactionHistory();
                Console.WriteLine("----------------");
            }
        }
    }
}
