using CompanyEFCore.Models;

namespace CompanyEFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Context= new CompanyContext();
            var emp = new Employee() { Ssn = 50, Fname = "yara", Lname = "salah" };
            Context.Employees.Add(emp);
            Context.SaveChanges();
            var employees = Context.Employees.ToList();
            foreach (var e in employees)
            {
                Console.WriteLine($"{e.Fname} - {e.Lname}");
            }
            var dep = Context.Departments.FirstOrDefault();
            Console.WriteLine(dep.Dname);
            var loc=Context.DepartmentLocations.First();
            Console.WriteLine(loc.Location);
            var projects=Context.Projects.ToList();
            foreach (var p in projects)
            {
                Console.WriteLine($"{p.Pnumber} - {p.Pname}");
            }

        }
    }
}
