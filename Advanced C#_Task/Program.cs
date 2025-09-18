namespace Advanced_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //1.Create a PhoneBook class with an indexer that takes a person's name and returns their phone number.

            PhoneBook phoneBook = new PhoneBook();
            phoneBook.Add("Menna", "123-456-6792");
            phoneBook.Add("Huda", "345-578-5682");
            Console.WriteLine("Menna's number: " + phoneBook["Menna"]);
            Console.WriteLine("Huda's number: " + phoneBook["Huda"]);


            //2.Build a WeeklySchedule class where you can access daily schedules using day names: schedule["Monday"].
            WeeklySchedule schedule = new WeeklySchedule();

            // Test setting schedules using indexer
            //schedule["Monday"] = "Coaching 6-9";
            //schedule["Tuesday"] = "College at 6 AM";
            //schedule["Saturday"] = "Tech Session 10-1";
            //Console.WriteLine("Monday: " + schedule["Monday"]);
            //Console.WriteLine("Tuesday: " + schedule["Tuesday"]);
            //Console.WriteLine("Wednesday: " + schedule["Wednesday"]);
        }

    }
}

