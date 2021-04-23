using Microsoft.AspNetCore.Components;
using Organisation.WebAssembly.App.Models;
using Organisation.WebAssembly.App.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organisation.WebAssembly.App.Pages
{
    public partial class EmployeeList : ComponentBase
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetEmployees()).ToList();
        }
    }
}
