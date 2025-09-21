using Task1.Data;

namespace Task1.Interfaces
{
    public interface IEmplyeeService
    {
        Task<IEnumerable<Employee>>GetAll();
        Task<Employee>CreateNewone(Employee employee);
        Task<Employee> GetBySSN(string SSN);
        Task<Employee> UpdateEmp(string SSN,Employee employee);
        Task<Employee> DeleteEmp(string SSN);
    }
}
