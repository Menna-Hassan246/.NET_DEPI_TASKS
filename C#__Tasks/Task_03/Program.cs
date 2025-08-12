namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // BankAccount   account1 = new BankAccount(0,
            //    "Menna Hassan",
            //     "12345678912343",
            //    "01000000000",
            //    "Tanta");

            //BankAccount account2 = new BankAccount(1,
            //    "Huda Hassan",
            //    "32456167895432",
            //    "01207665787",
            //    "Cairo",
            //   1000);
            //account1.ShowAccountDetails();
            //account2.ShowAccountDetails();

          
            // Create objects of both account types
            SavingAccount savingAccount = new SavingAccount(1001, 
                "Menna Hassan",
                 "12345678912343",
                "01000000000",
                "Tanta", 5000, 2.5m);
            CurrentAccount currentAccount = new CurrentAccount(2001, "Huda Hassan",
                "32456167895432",
                "01207665787",
                "Cairo",
               1000, 2000);
            savingAccount.ShowAccountDetails();
            currentAccount.ShowAccountDetails();
            // Add both to a List<BankAccount>
            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(savingAccount);
            accounts.Add(currentAccount);

            //Loop through accounts and call methods
            foreach (BankAccount account in accounts)
            {
                account.ShowAccountDetails();
                
                Console.WriteLine($"Calculated Interest: {account.CalculateInterest()}");
                
                Console.WriteLine("==============================================================");
            }
        }
    }
    }

