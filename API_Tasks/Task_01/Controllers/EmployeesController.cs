using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.Data;

namespace Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public EmployeesController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetallEmplyees()
        {
            var emps=await _db.Employees.ToListAsync();
            return Ok(emps);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewEmp(string ssn,string F,string L,decimal s)
        {
            Employee emp = new() { SSN = ssn,FName=F,LName=L,Salary=s };
            await _db.Employees.AddAsync(emp);
            _db.SaveChanges();
            return Ok(emp);
        }
        [HttpPut]
        public async Task<IActionResult>UpdateEmpSalary(string ssn,Employee e)
        {
            var emp= await _db.Employees.FindAsync(ssn);
            if (emp == null)
            {
                return NotFound($"Employee SSN {ssn} not exist ");
            }
            emp.Salary = e.Salary;
            _db.SaveChangesAsync();
            return Ok(emp);
        }
        [HttpDelete("SSN")]
        public async Task<IActionResult>DeleteEmp(string ssn)
        {
            var emp=await _db.Employees.FindAsync(ssn);
            if (emp == null)
            {
                return NotFound($"Employee SSN {ssn} not exist ");
            }
            _db.Employees.Remove(emp);
            _db.SaveChangesAsync();
             return Ok(emp) ;
        }
    }
}
