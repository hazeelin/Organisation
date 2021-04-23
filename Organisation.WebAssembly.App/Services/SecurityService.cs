using Organisation.WebAssembly.App.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organisation.WebAssembly.App.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly HttpClient httpClient;

        public SecurityService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Token> GetToken()
        {
            return await JsonSerializer.DeserializeAsync<Token>
                (await httpClient.GetStreamAsync($"/main/gettoken"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
