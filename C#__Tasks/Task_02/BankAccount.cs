using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task2
{
    internal class BankAccount
    {
       private  const string BankCode = "BNK001";
       public  readonly DateTime CreatedDate;
       private int _accountNumber;

        private int _balance;
        private string _address;
       private string _phoneNumber ;
       private string _nationalID;
        private string __fullName;

        
        public string FullName
        {
            get { return __fullName; } // Getter
            set {
                if (value != null || value != "")
                    __fullName = value;
                else
                    Console.WriteLine("invalid name");
                    } // Setter
        }

        public string NationalID
        {
            get { return _nationalID; } // Getter
            set
            {
                if (IsValidNationalID(value))
                    _nationalID = value;
                else
                    Console.WriteLine("invalid Id");
            }

        }
        public string PhoneNumber
        {
            get { return _phoneNumber; } // Getter

            set
            {
                if (IsValidPhoneNumber(value))
                    _phoneNumber = value;
                else
                    Console.WriteLine("invalid number");
            }
            // Setter
        }
        public int Balance
        {
            get { return _balance; } // Getter
            set
            {
                if (value>=0)
                    _balance = value;
                else
                    Console.WriteLine("invalid balance");
            } // Setter
        }
        public string Address
        {
            get { return _address; } // Getter
            set
            { _address = value; }   
        }
      
        
public BankAccount()
        {
            Console.WriteLine("Default Constructor");
            CreatedDate= DateTime.Now;
            _accountNumber= 0;
            FullName= "nobody";
            NationalID= "12345612345612";
           PhoneNumber = "01000000000";
          Address = "Alex";
         Balance = 0;
        
        }
        public BankAccount(int acc, string fullname, string national_ID, string phonenumber, string address)
        {
            _accountNumber = 0;
            Address = address;
             Balance =0; 
             PhoneNumber = phonenumber;
              NationalID = national_ID;
                FullName = fullname;

        }
        public BankAccount(int acc,string fullname,  string national_ID, string phonenumber, string address,int balance):this(acc,fullname, national_ID, phonenumber, address)
        { 

            _balance = balance;
           
        }
       public void showAccountDetails()
        {
            Console.WriteLine($"All data:\n CreateDtae:{CreatedDate}," +
                $"\nBalance:{Balance}," +
                $" \nAddress:{Address}," +
                $"\nPhonenumber:{PhoneNumber}," +
                $"\nNationalID:{NationalID}," +
                $"\nFullNAME:{FullName}," +
                $"\nBankcode:{BankCode}" +
                $"\n______________________________________________________________");
        }
      
        public bool IsValidPhoneNumber(string number)
        {
            return !string.IsNullOrEmpty(number) && number.Length == 11 && number.StartsWith("01");
        }

        public bool IsValidNationalID(string id)
        {
            if (!string.IsNullOrEmpty(id) && id.Length == 14)
                return true;
            else
                return false;
        }

    }
}
