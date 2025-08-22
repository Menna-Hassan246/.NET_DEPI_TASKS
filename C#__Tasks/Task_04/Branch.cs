using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement
{
    public class Branch
    {
        private string _BranchCode;
        public Branch()
        {
            _BranchCode = "Benha$1234";

        }
        public string BranchCode
        {
            get
            {
                return _BranchCode;
            }
        }
        
    } }
    
