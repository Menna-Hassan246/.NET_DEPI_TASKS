using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
    public class Bank
    {
        public string Name { get; set; }
        public Branch Branch { get; set; }
        private List<Customer> _customers;

        public Bank(string name, Branch branch)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Bank name cannot be empty.");
                Name = "Default Bank";
            }
            else
            {
                Name = name;
            }
            if (branch == null)
            {
                Console.WriteLine("Branch cannot be null.");
                Branch = new Branch();
            }
            else
            {
                Branch = branch;
            }
            _customers = new List<Customer>();
            Console.WriteLine($"{Name}\nOur code is: {Branch.BranchCode}\n----------------------------------------------------------------");
        }

        public bool AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                Console.WriteLine("Customer cannot be null.");
                return false;
            }
            for (int i = 0; i < _customers.Count; i++)
            {
                if (_customers[i].NationalID == customer.NationalID)
                {
                    Console.WriteLine("Customer with this National ID already exists.");
                    return false;
                }
            }
            _customers.Add(customer);
            return true;
        }

        public bool RemoveCustomer(string nationalID)
        {
            Customer customer = SearchCustomer(nationalID);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return false;
            }
            for (int i = 0; i < customer.Accounts.Count; i++)
            {
                if (customer.Accounts[i].CurrentBalance != 0)
                {
                    Console.WriteLine("Cannot remove customer: Non-zero balance in accounts.");
                    return false;
                }
            }
            _customers.Remove(customer);
            Console.WriteLine($"Customer {customer.Fullname} removed.");
            return true;
        }

        public Customer SearchCustomer(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                Console.WriteLine("Search term cannot be empty.");
                return null;
            }
            for (int i = 0; i < _customers.Count; i++)
            {
                if (_customers[i].NationalID == searchTerm || _customers[i].Fullname.ToLower().Contains(searchTerm.ToLower()))
                {
                    return _customers[i];
                }
            }
            Console.WriteLine("Customer not found.");
            return null;
        }

        public void ShowAllCustomers()
        {
            if (_customers.Count == 0)
            {
                Console.WriteLine("No customers in the bank.");
                return;
            }
            Console.WriteLine("Bank Report:");
            for (int i = 0; i < _customers.Count; i++)
            {
                _customers[i].DisplayCustomer();
                Console.WriteLine("Accounts:");
                for (int j = 0; j < _customers[i].Accounts.Count; j++)
                {
                    string accountType = _customers[i].Accounts[j] is SavAccount ? "Savings" : "Current";
                    Console.WriteLine($"  Account {_customers[i].Accounts[j].AccountNumber} ({accountType}): Balance = {_customers[i].Accounts[j].CurrentBalance:C}, Opened = {_customers[i].Accounts[j].DateOpened.ToShortDateString()}");
                }
                Console.WriteLine("----------------------------------------------------------------");
            }
        }

        public void DisplayAllTransactionHistory()
        {
            Console.WriteLine("Transaction History for All Customers:");
            if (_customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
                return;
            }
            for (int i = 0; i < _customers.Count; i++)
            {
                _customers[i].DisplayAllTransactionHistory();
                Console.WriteLine("========================================");
            }
        }
    }
}



