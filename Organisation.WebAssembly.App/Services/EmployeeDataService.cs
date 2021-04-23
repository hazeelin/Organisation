using Organisation.WebAssembly.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organisation.WebAssembly.App.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient httpClient;
        private readonly ISecurityService securityService;

        public Token token { get; set; } = new Token();

        public EmployeeDataService(HttpClient httpClient, ISecurityService securityService)
        {
            this.httpClient = httpClient;
            this.securityService = securityService;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {

            var employeeJson =
                new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/employee", employeeJson);


            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Employee>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await httpClient.DeleteAsync($"/employee/{employeeId}");
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await JsonSerializer.DeserializeAsync<Employee>
                (await httpClient.GetStreamAsync($"/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                await GetToken();
                SetHeaderWithToken();
                return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
                    (await httpClient.GetStreamAsync($"/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task UpdateEmployee(Employee employee)
        {
            var employeeId = employee.EmployeeId;
            var employeeJson =
                new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            await httpClient.PatchAsync($"/employee/{employeeId}", employeeJson);
        }

        protected async Task GetToken()
        {
            token = await securityService.GetToken();
        }

        private void SetHeaderWithToken()
        {
            if (token != null)
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Data}");
        }
    }
}
