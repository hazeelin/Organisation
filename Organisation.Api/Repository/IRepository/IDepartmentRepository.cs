using Organisation.Api.Models;
using System.Collections.Generic;

namespace Organisation.Api.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetDepartments();
        Department GetDepartment(int id);
    }
}
