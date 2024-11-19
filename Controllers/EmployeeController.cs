using Microsoft.AspNetCore.Mvc;
using Emp_management.ServiceLayer;
using Emp_management.DataLayer.DTOs;
using Emp_management.DataLayer.Models;
using Microsoft.Extensions.Logging;
using Emp_management.ServiceLayer.Validation;

namespace Emp_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult GetEmployees(
             [FromQuery] string? name,
             [FromQuery] DateTime? doj,
             [FromQuery] string? email,
             [FromQuery] EmployeeStatus status = EmployeeStatus.Active) // Default to Active
        {
            var employees = _employeeService.GetEmployees(name, doj, email, status);
            return Ok(employees);
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeService.GetEmployee(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee([FromBody] EmployeeCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Employee data is required");
            }

            // Call the ValidationService to validate the input
            ValidationService.ValidateEmployeeCreateDto(dto, ModelState);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            _employeeService.CreateEmployee(dto);
            return Ok(new { Message = "Employee created successfully!" });
        }

        // PUT: api/Employee
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEmployee = _employeeService.GetEmployee(dto.EmpId);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.UpdateEmployee(dto);
            return NoContent();
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var existingEmployee = _employeeService.GetEmployee(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.DeleteEmployee(id);
            return NoContent();
        }
    }
}
