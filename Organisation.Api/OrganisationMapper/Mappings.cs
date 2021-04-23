using AutoMapper;
using Organisation.Api.Models;
using Organisation.Api.Models.Dtos;

namespace Organisation.Api.OrganisationMapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
