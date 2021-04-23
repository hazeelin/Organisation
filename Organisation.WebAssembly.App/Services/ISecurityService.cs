using Organisation.WebAssembly.App.Models;
using System.Threading.Tasks;

namespace Organisation.WebAssembly.App.Services
{
    public interface ISecurityService
    {
        Task<Token> GetToken();
    }
}