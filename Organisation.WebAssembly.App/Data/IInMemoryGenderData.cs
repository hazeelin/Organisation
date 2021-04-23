using Organisation.WebAssembly.App.Models;
using System.Collections.Generic;

namespace Organisation.WebAssembly.App.Data
{
    public interface IInMemoryGenderData
    {
        IEnumerable<Gender> GetGender(string name = null);
    }
}