using Organisation.WebAssembly.App.Models;
using System.Collections.Generic;
using System.Linq;

namespace Organisation.WebAssembly.App.Data
{
    public class InMemoryGenderData : IInMemoryGenderData
    {
        List<Gender> genderList;

        public InMemoryGenderData()
        {
            genderList = new List<Gender>()
            {
                new Gender { Code = "F", Name = "Female"},
                new Gender { Code = "M", Name = "Male"}
            };
        }

        public IEnumerable<Gender> GetGender(string name = null)
        {
            return from r in genderList
                   where string.IsNullOrEmpty(name) || r.Name.ToLower().Contains(name)
                   orderby r.Name
                   select r;
        }
    }
}
