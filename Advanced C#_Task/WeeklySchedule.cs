using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_C_
{
    internal class WeeklySchedule
    {

        private Dictionary<string, string> schedules = new Dictionary<string, string>()
    {
        {"Monday", ""},
        {"Tuesday", ""},
        {"Wednesday", ""},
        {"Thursday", ""},
        {"Friday", ""},
        {"Saturday", ""},
        {"Sunday", ""}
    };
        // Indexer to access daily schedules
        public string this[string day]
        {
            get
            {
                if (schedules.ContainsKey(day))
                    return schedules[day];
                     return "No Schedule";
            }
            set
            {
                if (schedules.ContainsKey(day))
                    schedules[day] = value;
                else
                   Console.WriteLine("No Schedule");
            }
        }

    }
}