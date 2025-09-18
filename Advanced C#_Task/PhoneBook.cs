using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_C_
{
    internal class PhoneBook
    {
        private Dictionary<string, string> contacts = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                if (contacts.ContainsKey(name))
                    return contacts[name];
                return "Not found";
            }
            set
            {
                contacts[name] = value;
            }
        }

        // Add a contact
        public void Add(string name, string phoneNumber)
        {
            contacts[name] = phoneNumber;
        }
      
    }

}
