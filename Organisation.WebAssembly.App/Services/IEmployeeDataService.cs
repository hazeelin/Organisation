using Organisation.WebAssembly.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Organisation.WebAssembly.App.Services
{
    public interface IEmployeeDataService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);

    }
}
