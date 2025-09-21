using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.Data;
using Task1.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmplyeeService _Service;
        public EmployeesController(IEmplyeeService Service)
        {
            _Service = Service;
        }
        [HttpGet]
        public async Task<IActionResult> GetallEmplyees()
        {
            var emps=await _Service.GetAll();
            return Ok(emps);
        }
        [HttpGet("SSN")]
        public async Task<IActionResult> GetBySSN(string SSN)
        {
            var emps = await _Service.GetBySSN(SSN);
            return Ok(emps);
        }
        //SEARCHING BY ROUTE
        [HttpGet("search/route/{SSN}")]
        public async Task<IActionResult> GetByRoute( [FromRoute] string SSN)
        {
            var emps = await _Service.GetBySSN(SSN);
            return Ok(emps);
        }
        //Searching by Query
        [HttpGet("search / query")]
        public async Task<IActionResult> SearchByQuery([FromQuery] string SSN)
        {
            var emps = await _Service.GetBySSN(SSN);
            return Ok(emps);
        }
        //Searching by Form
        [HttpPost("search /form")]
        public async Task<IActionResult> SearchByForm([FromForm] string SSN)
        {
            var emps = await _Service.GetBySSN(SSN);
            return Ok(emps);
        }
        //Searching by Body
        [HttpPost("search /body")]
        public async Task<IActionResult> SearchByBody([FromBody] string SSN)
        {
            var emps = await _Service.GetBySSN(SSN);
            return Ok(emps);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewEmp(Employee employee)
        {
            Employee emp = new Employee()
            { SSN = employee.SSN,FName=employee.FName,LName=employee.LName,Salary=employee.Salary };
            await _Service.CreateNewone(employee);
            return Ok(emp);
        }
        [HttpPut]
        public async Task<IActionResult>UpdateEmpSalary(string ssn,Employee e)
        {
            var emp= await _Service.UpdateEmp(ssn,e);
            if (emp == null)
            {
                return NotFound($"Employee SSN {ssn} not exist ");
            }
            emp.Salary = e.Salary;
            return Ok(emp);
        }
        [HttpDelete("SSN")]
        public async Task<IActionResult>DeleteEmp(string ssn)
        {
            var emp=await _Service.DeleteEmp(ssn);
            if (emp == null)    
            {
                return NotFound($"Employee SSN {ssn} not exist ");
            }
             return Ok(emp) ;
        }
    }
}
