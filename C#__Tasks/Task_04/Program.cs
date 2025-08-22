using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
    class Program
    {
        static Bank bank;
        static void Main()
        {
            // Initialize bank and branch
            Branch branch = new Branch();
            bank = new Bank("Benha Bank", branch);

            // Sample initial data
            Customer c1 = new Customer("Ali  Hassan", "ID123", new DateTime(1990, 5, 15));
            Customer c2 = new Customer("aya  Hassan", "ID456", new DateTime(1985, 8, 20));
            bank.AddCustomer(c1);
            bank.AddCustomer(c2);
            SavAccount s1 = new SavAccount(3.5m);
            CurrentAccount c1a = new CurrentAccount(500m);
            c1.AddAccount(s1);
            c1.AddAccount(c1a);
            c2.AddAccount(new SavAccount(2.0m));
            s1.Deposit(1000);
            s1.Withdraw(200);
            s1.ApplyMonthlyInterest();
            c1a.Deposit(500);
            c1a.Transfer(s1, 300);

            // Main menu loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome to {bank.Name}");
                Console.WriteLine("------------------------");
                Console.WriteLine("1. Customer Management");
                Console.WriteLine("2. Bank Management");
                Console.WriteLine("3. Account Management");
                Console.WriteLine("4. Reporting");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option (1-5): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CustomerManagementMenu();
                        break;
                    case "2":
                        BankManagementMenu();
                        break;
                    case "3":
                        AccountManagementMenu();
                        break;
                    case "4":
                        ReportingMenu();
                        break;
                    case "5":
                        Console.WriteLine("Thank you for using Benha Bank. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void CustomerManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Customer Management");
                Console.WriteLine("------------------");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Update Customer");
                Console.WriteLine("3. Remove Customer");
                Console.WriteLine("4. View Customer Details");
                Console.WriteLine("5. View All Transaction History for Customer");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option (1-6): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Full Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter National ID: ");
                        string nationalId = Console.ReadLine();
                        Console.Write("Enter Birth Date (yyyy-mm-dd): ");
                        DateTime birth;
                        if (DateTime.TryParse(Console.ReadLine(), out birth))
                        {
                            Customer c = new Customer(name, nationalId, birth);
                            if (bank.AddCustomer(c))
                                Console.WriteLine("Customer added successfully.");
                            else
                                Console.WriteLine("Failed to add customer.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                        break;
                    case "2":
                        Console.Write("Enter National ID to Update: ");
                        string idToUpdate = Console.ReadLine();
                        Customer found = bank.SearchCustomer(idToUpdate);
                        if (found != null)
                        {
                            Console.Write("Enter New Name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Enter New Birth Date (yyyy-mm-dd): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime newBirth))
                            {
                                if (found.Update(newName, newBirth))
                                    Console.WriteLine("Customer updated.");
                                else
                                    Console.WriteLine("Update failed.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Customer not found.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter National ID to Remove: ");
                        string idToRemove = Console.ReadLine();
                        if (bank.RemoveCustomer(idToRemove))
                            Console.WriteLine("Customer removed.");
                        else
                            Console.WriteLine("Failed to remove customer.");
                        break;
                    case "4":
                        Console.Write("Enter National ID to View: ");
                        string idToView = Console.ReadLine();
                        Customer cust = bank.SearchCustomer(idToView);
                        if (cust != null)
                            cust.DisplayCustomer();
                        else
                            Console.WriteLine("Customer not found.");
                        break;
                    case "5":
                        Console.Write("Enter National ID to View History: ");
                        string idForHistory = Console.ReadLine();
                        Customer custWithHistory = bank.SearchCustomer(idForHistory);
                        if (custWithHistory != null)
                            custWithHistory.DisplayAllTransactionHistory();
                        else
                            Console.WriteLine("Customer not found.");
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void BankManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bank Management");
                Console.WriteLine("---------------");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Remove Customer");
                Console.WriteLine("3. Search Customer");
                Console.WriteLine("4. View All Customers");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select an option (1-5): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Full Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter National ID: ");
                        string nationalId = Console.ReadLine();
                        Console.Write("Enter Birth Date (yyyy-mm-dd): ");
                        DateTime birth;
                        if (DateTime.TryParse(Console.ReadLine(), out birth))
                        {
                            Customer c = new Customer(name, nationalId, birth);
                            if (bank.AddCustomer(c))
                                Console.WriteLine("Customer added successfully.");
                            else
                                Console.WriteLine("Failed to add customer.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                        break;
                    case "2":
                        Console.Write("Enter National ID to Remove: ");
                        string idToRemove = Console.ReadLine();
                        if (bank.RemoveCustomer(idToRemove))
                            Console.WriteLine("Customer removed.");
                        else
                            Console.WriteLine("Failed to remove customer.");
                        break;
                    case "3":
                        Console.Write("Enter Search Term (National ID or Name): ");
                        string searchTerm = Console.ReadLine();
                        Customer found = bank.SearchCustomer(searchTerm);
                        if (found != null)
                            found.DisplayCustomer();
                        else
                            Console.WriteLine("Customer not found.");
                        break;
                    case "4":
                        bank.ShowAllCustomers();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void AccountManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Account Management");
                Console.WriteLine("------------------");
                Console.WriteLine("1. Create Savings Account");
                Console.WriteLine("2. Create Current Account");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. View Transaction History");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("Select an option (1-7): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Customer National ID: ");
                        string custId = Console.ReadLine();
                        Customer cust = bank.SearchCustomer(custId);
                        if (cust != null)
                        {
                            Console.Write("Enter Interest Rate (%): ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal rate) && rate > 0)
                            {
                                SavAccount sa = new SavAccount(rate);
                                if (cust.AddAccount(sa))
                                    Console.WriteLine("Savings account created.");
                                else
                                    Console.WriteLine("Failed to add account.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid interest rate.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Customer not found.");
                        }
                        break;
                    case "2":
                        Console.Write("Enter Customer National ID: ");
                        string custId2 = Console.ReadLine();
                        Customer cust2 = bank.SearchCustomer(custId2);
                        if (cust2 != null)
                        {
                            Console.Write("Enter Overdraft Limit: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal limit) && limit >= 0)
                            {
                                CurrentAccount ca = new CurrentAccount(limit);
                                if (cust2.AddAccount(ca))
                                    Console.WriteLine("Current account created.");
                                else
                                    Console.WriteLine("Failed to add account.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid overdraft limit.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Customer not found.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter Customer National ID: ");
                        string custIdDep = Console.ReadLine();
                        Customer custDep = bank.SearchCustomer(custIdDep);
                        if (custDep != null)
                        {
                            if (custDep.Accounts.Count > 0)
                            {
                                Console.WriteLine("Available Accounts:");
                                for (int i = 0; i < custDep.Accounts.Count; i++)
                                {
                                    string accountType = custDep.Accounts[i] is SavAccount ? "Savings" : "Current";
                                    Console.WriteLine($"{i + 1}. Account {custDep.Accounts[i].AccountNumber} ({accountType})");
                                }
                                Console.Write("Select Account Number (1-{0}): ", custDep.Accounts.Count);
                                if (int.TryParse(Console.ReadLine(), out int accIndex) && accIndex > 0 && accIndex <= custDep.Accounts.Count)
                                {
                                    Account acc = custDep.Accounts[accIndex - 1];
                                    Console.Write("Enter Amount to Deposit: ");
                                    if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                                    {
                                        if (acc.Deposit(amount))
                                            Console.WriteLine("Deposit successful.");
                                        else
                                            Console.WriteLine("Deposit failed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid amount.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid selection.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No accounts found for this customer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Customer not found.");
                        }
                        break;
                    case "4":
                        Console.Write("Enter Customer National ID: ");
                        string custIdWith = Console.ReadLine();
                        Customer custWith = bank.SearchCustomer(custIdWith);
                        if (custWith != null)
                        {
                            if (custWith.Accounts.Count > 0)
                            {
                                Console.WriteLine("Available Accounts:");
                                for (int i = 0; i < custWith.Accounts.Count; i++)
                                {
                                    string accountType = custWith.Accounts[i] is SavAccount ? "Savings" : "Current";
                                    Console.WriteLine($"{i + 1}. Account {custWith.Accounts[i].AccountNumber} ({accountType})");
                                }
                                Console.Write("Select Account Number (1-{0}): ", custWith.Accounts.Count);
                                if (int.TryParse(Console.ReadLine(), out int accIndex) && accIndex > 0 && accIndex <= custWith.Accounts.Count)
                                {
                                    Account acc = custWith.Accounts[accIndex - 1];
                                    Console.Write("Enter Amount to Withdraw: ");
                                    if (decimal.TryParse(Console.ReadLine(), out decimal amountW) && amountW > 0)
                                    {
                                        if (acc.Withdraw(amountW))
                                            Console.WriteLine("Withdrawal successful.");
                                        else
                                            Console.WriteLine("Withdrawal failed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid amount.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid selection.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No accounts found for this customer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Customer not found.");
                        }
                        break;
                    case "5":
                        Console.Write("Enter Source Customer National ID: ");
                        string srcCustId = Console.ReadLine();
                        Customer srcCust = bank.SearchCustomer(srcCustId);
                        if (srcCust != null)
                        {
                            if (srcCust.Accounts.Count > 0)
                            {
                                Console.WriteLine("Source Accounts:");
                                for (int i = 0; i < srcCust.Accounts.Count; i++)
                                {
                                    string accountType = srcCust.Accounts[i] is SavAccount  ? "Savings" : "Current";
                                    Console.WriteLine($"{i + 1}. Account {srcCust.Accounts[i].AccountNumber} ({accountType})");
                                }
                                Console.Write("Select Source Account Number (1-{0}): ", srcCust.Accounts.Count);
                                if (int.TryParse(Console.ReadLine(), out int srcAccIndex) && srcAccIndex > 0 && srcAccIndex <= srcCust.Accounts.Count)
                                {
                                    Account srcAcc = srcCust.Accounts[srcAccIndex - 1];
                                    Console.Write("Enter Target Customer National ID: ");
                                    string tgtCustId = Console.ReadLine();
                                    Customer tgtCust = bank.SearchCustomer(tgtCustId);
                                    if (tgtCust != null)
                                    {
                                        if (tgtCust.Accounts.Count > 0)
                                        {
                                            Console.WriteLine("Target Accounts:");
                                            for (int i = 0; i < tgtCust.Accounts.Count; i++)
                                            {
                                                string accountType = tgtCust.Accounts[i] is SavAccount ? "Savings" : "Current";
                                                Console.WriteLine($"{i + 1}. Account {tgtCust.Accounts[i].AccountNumber} ({accountType})");
                                            }
                                            Console.Write("Select Target Account Number (1-{0}): ", tgtCust.Accounts.Count);
                                            if (int.TryParse(Console.ReadLine(), out int tgtAccIndex) && tgtAccIndex > 0 && tgtAccIndex <= tgtCust.Accounts.Count)
                                            {
                                                Account tgtAcc = tgtCust.Accounts[tgtAccIndex - 1];
                                                Console.Write("Enter Amount to Transfer: ");
                                                if (decimal.TryParse(Console.ReadLine(), out decimal amountT) && amountT > 0)
                                                {
                                                    if (srcAcc.Transfer(tgtAcc, amountT))
                                                        Console.WriteLine("Transfer successful.");
                                                    else
                                                        Console.WriteLine("Transfer failed.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid amount.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid target account selection.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No accounts found for target customer.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Target customer not found.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid source account selection.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No accounts found for source customer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Source customer not found.");
                        }
                        break;
                    case "6":
                        Console.Write("Enter Customer National ID: ");
                        string custIdHist = Console.ReadLine();
                        Customer custHist = bank.SearchCustomer(custIdHist);
                        if (custHist != null)
                        {
                            if (custHist.Accounts.Count > 0)
                            {
                                Console.WriteLine("Available Accounts:");
                                for (int i = 0; i < custHist.Accounts.Count; i++)
                                {
                                    string accountType = custHist.Accounts[i] is SavAccount ? "Savings" : "Current";
                                    Console.WriteLine($"{i + 1}. Account {custHist.Accounts[i].AccountNumber} ({accountType})");
                                }
                                Console.Write("Select Account Number (1-{0}): ", custHist.Accounts.Count);
                                if (int.TryParse(Console.ReadLine(), out int accIndexHist) && accIndexHist > 0 && accIndexHist <= custHist.Accounts.Count)
                                {
                                    Account acc = custHist.Accounts[accIndexHist - 1];
                                    acc.DisplayTransactionHistory();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid selection.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No accounts found for this customer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Customer not found.");
                        }
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void ReportingMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Reporting");
                Console.WriteLine("---------");
                Console.WriteLine("1. View All Customers");
                Console.WriteLine("2. View All Transaction History");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select an option (1-3): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        bank.ShowAllCustomers();
                        break;
                    case "2":
                        bank.DisplayAllTransactionHistory();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
