using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Organisation.Api.Repository.IRepository;

namespace Organisation.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        // GET: /<controller>
        [HttpGet]
        public IActionResult GetDepartments()
        {
            return Ok(departmentRepository.GetDepartments());
        }

        // GET /<controller>/5
        [HttpGet("{id:int}", Name = "GetDepartment")]
        public IActionResult GetDepartment(int id)
        {
            return Ok(departmentRepository.GetDepartment(id));
        }
    }
}
