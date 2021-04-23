using Organisation.WebAssembly.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organisation.WebAssembly.App.Services
{
    public class DepartmentDataService : IDepartmentDataService
    {
        private readonly HttpClient httpClient;

        public DepartmentDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<Department>
                    (await httpClient.GetStreamAsync($"/department"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Department>>
                    (await httpClient.GetStreamAsync($"/department"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
