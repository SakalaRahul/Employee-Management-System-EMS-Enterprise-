using EmployeeManagementSystem.API.DTOs;
using EmployeeManagementSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] string? search, [FromQuery] int? departmentId, [FromQuery] string? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (items, totalCount) = await _employeeService.GetEmployeesAsync(search, departmentId, status, page, pageSize);
            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                Data = items
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound(new { Message = $"Employee with ID {id} not found." });
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _employeeService.CreateEmployeeAsync(dto);
            return CreatedAtAction(nameof(GetEmployee), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _employeeService.UpdateEmployeeAsync(id, dto);
            if (!success) return NotFound(new { Message = $"Employee with ID {id} not found." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var success = await _employeeService.DeleteEmployeeAsync(id);
            if (!success) return NotFound(new { Message = $"Employee with ID {id} not found." });

            return NoContent();
        }
    }
}