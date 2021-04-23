using Organisation.Api.Models;
using System.Collections.Generic;

namespace Organisation.Api.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();

        Employee GetEmployee(int employeeId);

        bool CreateEmployee(Employee employee);

        bool UpdateEmployee(Employee employee);

        bool DeleteEmployee(Employee employee);

        bool EmployeeIdExists(int employeeId);

        bool Save();
    }
}
