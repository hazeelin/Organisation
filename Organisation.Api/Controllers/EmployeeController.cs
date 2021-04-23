using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organisation.Api.Models;
using Organisation.Api.Models.Dtos;
using Organisation.Api.Repository.IRepository;
using System.Collections.Generic;

namespace Organisation.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;


        }

        /// <summary>
        /// Get a list of all employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmployeeDto>))]
        public IActionResult GetEmployees()
        {
            var employeeList = employeeRepository.GetEmployees();

            var objDto = new List<EmployeeDto>();

            foreach (var employee in employeeList)
            {
                objDto.Add(mapper.Map<EmployeeDto>(employee));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id">Id of the employee.</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        //[Authorize]     //--(AuthenticationSchemes = "JwtBearerDefaults.AuthenticationScheme")]
        public IActionResult GetEmployee(int id)
        {
            var employee = employeeRepository.GetEmployee(id);

            if (employee == null)
                return NotFound();

            var objDto = mapper.Map<EmployeeDto>(employee);


            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeDto))]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest(ModelState);

            var employee = mapper.Map<Employee>(employeeDto);

            if (!employeeRepository.CreateEmployee(employee))
            {
                ModelState.AddModelError("", $"Oops there is an issue when saving {employeeDto.FirstName} {employeeDto.LastName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        [HttpPatch("{id:int}", Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null || id != employeeDto.EmployeeId)
                return BadRequest(ModelState);

            if (!employeeRepository.EmployeeIdExists(employeeDto.EmployeeId))
            {
                ModelState.AddModelError("", "Employee Id: " + employeeDto.EmployeeId + " does not exists");
                return StatusCode(404, ModelState);
            }

            var employee = mapper.Map<Employee>(employeeDto);

            if (!employeeRepository.UpdateEmployee(employee))
            {
                ModelState.AddModelError("", $"Oops there is an issue when updating employee id {employeeDto.EmployeeId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteEmployee(int id)
        {
            if (!employeeRepository.EmployeeIdExists(id))
            {
                return NotFound();
            }

            var employee = employeeRepository.GetEmployee(id);
            if (!employeeRepository.DeleteEmployee(employee))
            {
                ModelState.AddModelError("", $"Oops there is an issue when deleting employee id {employee.EmployeeId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
