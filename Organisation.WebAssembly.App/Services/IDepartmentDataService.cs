using Organisation.WebAssembly.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Organisation.WebAssembly.App.Services
{
    public interface IDepartmentDataService
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int departmentId);
    }
}
