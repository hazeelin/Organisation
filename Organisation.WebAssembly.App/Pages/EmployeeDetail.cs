using Microsoft.AspNetCore.Components;
using Organisation.WebAssembly.App.Models;
using Organisation.WebAssembly.App.Services;
using System.Threading.Tasks;

namespace Organisation.WebAssembly.App.Pages
{
    public partial class EmployeeDetail : ComponentBase
    {
        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();


        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeDataService.GetEmployee(int.Parse(EmployeeId));
        }
    }
}
