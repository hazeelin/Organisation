using Organisation.Api.Data;
using Organisation.Api.Models;
using Organisation.Api.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Organisation.Api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool CreateEmployee(Employee employee)
        {
            db.Employee.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            db.Employee.Remove(employee);
            return Save();
        }

        public Employee GetEmployee(int employeeId)
        {
            return db.Employee.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public ICollection<Employee> GetEmployees()
        {
            return db.Employee.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList();
        }

        public bool EmployeeIdExists(int employeeId)
        {
            return db.Employee.Any(Employee => Employee.EmployeeId == employeeId);
        }

        public bool Save()
        {
            try
            {
                return db.SaveChanges() >= 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateEmployee(Employee employee)
        {
            db.Employee.Update(employee);
            return Save();
        }
    }
}
