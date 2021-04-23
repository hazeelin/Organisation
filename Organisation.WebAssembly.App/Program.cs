using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Organisation.WebAssembly.App.Data;
using Organisation.WebAssembly.App.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace Organisation.WebAssembly.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri("https://localhost:44366"));
            builder.Services.AddHttpClient<ISecurityService, SecurityService>(client => client.BaseAddress = new Uri("https://localhost:44366"));
            builder.Services.AddSingleton<IInMemoryGenderData, InMemoryGenderData>();
            builder.Services.AddHttpClient<IDepartmentDataService, DepartmentDataService>(client => client.BaseAddress = new Uri("https://localhost:44366"));
            await builder.Build().RunAsync();
        }
    }
}
