using Organisation.Api.Data;
using Organisation.Api.Models;
using Organisation.Api.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Organisation.Api.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext db;

        public DepartmentRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Department GetDepartment(int id)
        {
            return db.Department.FirstOrDefault(d => d.Id == id);
        }

        public ICollection<Department> GetDepartments()
        {
            return db.Department.OrderBy(d => d.DepartmentName).ToList();
        }
    }
}
