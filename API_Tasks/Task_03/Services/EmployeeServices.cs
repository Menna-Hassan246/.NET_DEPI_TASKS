using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using Task1.Data;
using Task1.Interfaces;

namespace Task1.Services
{
    public class EmployeeServices : IEmplyeeService
    {
        private readonly AppDbContext _db;
        public EmployeeServices(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Employee> CreateNewone(Employee employee)
        {
            Employee emp = new() 
            { SSN = employee.SSN, FName = employee.FName, LName = employee.LName, Salary = employee.Salary };
            await _db.Employees.AddAsync(emp);
            _db.SaveChanges();
            return emp;
        }

       

        public async Task<Employee> DeleteEmp(string SSN)
        {
            var emp = await _db.Employees.FindAsync(SSN);
            if (emp == null)
            {
                return null;
            }
            _db.Employees.Remove(emp);
            _db.SaveChangesAsync();
            return emp;
        }
        
        public async Task<IEnumerable<Employee>> GetAll()
        {
            var emps = await _db.Employees.ToListAsync();
            return emps;
        }

        public async Task<Employee> GetBySSN(string SSN)
        {
            var emp = await _db.Employees.FindAsync(SSN);
            if (emp == null)
            {
                return null;
            }
                return emp;
            
        }
        public async Task<Employee> SearchByQuery( string SSN)
        {
            var emp = await _db.Employees.FindAsync(SSN);
            if (emp == null)
            {
                return null;
            }
            return emp;
        }
      
        public async Task<Employee> SearchByForm([FromForm] string SSN)
        {
            var emp = await _db.Employees.FindAsync(SSN);
            if (emp == null)
            {
                return null;
            }
            return emp;

        }
     
        public async Task<Employee> SearchByBody([FromBody] string SSN)
        {
            var emp = await _db.Employees.FindAsync(SSN);
            if (emp == null)
            {
                return null;
            }
            return emp;

        }


        public async Task<Employee> UpdateEmp(string SSN, Employee employee)
        {
            var emp = await _db.Employees.FindAsync(SSN);
            if (emp == null)
            {
                return null;
            }
            emp.Salary = employee.Salary;
            _db.SaveChangesAsync();
            return emp;
        }
    }
}
