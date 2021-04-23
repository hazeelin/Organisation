using Microsoft.AspNetCore.Components;
using Organisation.WebAssembly.App.Data;
using Organisation.WebAssembly.App.Models;
using Organisation.WebAssembly.App.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organisation.WebAssembly.App.Pages
{
    public partial class EmployeeEdit : ComponentBase
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Inject]
        public IDepartmentDataService DepartmentDataService { get; set; }

        [Inject]
        public IInMemoryGenderData InMemoryGenderData { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Employee Employee { get; set; } = new Employee();

        public List<Department> Departments { get; set; } = new List<Department>();

        public List<Gender> GenderList = new List<Gender>();

        protected string DepartmentId = string.Empty;

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;

            GenderList = InMemoryGenderData.GetGender().ToList();
            Departments = (await DepartmentDataService.GetDepartments()).ToList();

            int.TryParse(EmployeeId, out var employeeId);

            if (employeeId == 0) //new employee is being created
            {
            }
            else
            {
                Employee = await EmployeeDataService.GetEmployee(int.Parse(EmployeeId));
                DepartmentId = Employee.DepartmentId.ToString();
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;
            Employee.DepartmentId = int.Parse(DepartmentId);

            try
            {
                if (Employee.EmployeeId == 0) //new
                {
                    if (Employee.DepartmentId == 0)
                    {
                        StatusClass = "alert-danger";
                        Message = "Please enter the department id. Please try again.";
                        Saved = false;
                    }
                    else
                    {
                        var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
                        if (addedEmployee != null)
                        {
                            StatusClass = "alert-success";
                            Message = "New employee added successfully.";
                            Saved = true;
                        }
                        else
                        {
                            StatusClass = "alert-danger";
                            Message = "Something went wrong adding the new employee. Please try again.";
                            Saved = false;
                        }
                    }
                }
                else
                {
                    await EmployeeDataService.UpdateEmployee(Employee);
                    StatusClass = "alert-success";
                    Message = "Employee updated successfully.";
                    Saved = true;
                }
            }
            catch
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new employee. Please try again.";
                Saved = false;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeelist");
        }

    }
}
