namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount();
            account.showAccountDetails();
            BankAccount account1 = new BankAccount(
                acc:0,
                fullname:"menna hassan ",
                national_ID:"12345678912343",
               phonenumber:"01000000000",
               address:"Tanta");
            account1.showAccountDetails();
            BankAccount bankAccount = new BankAccount(acc: 1,
                fullname: "huda hassan",
                national_ID: "32456167895432",
               phonenumber: "01207665787",
               address: "Cairo",
               balance:1);
            bankAccount. showAccountDetails();
            
        }
    }
}
