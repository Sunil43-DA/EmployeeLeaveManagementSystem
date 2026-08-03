using EmployeeLeaveManagement.API.DTOs;
using EmployeeLeaveManagement.API.Helpers;
using EmployeeLeaveManagement.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
            [Authorize(Roles = "Admin")]
[HttpGet("test")]
public IActionResult Test()
{
    return Ok("Reached Controller");
}
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // ==========================
        // GET ALL EMPLOYEES
        // Admin and Manager Only
        // ==========================
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeQueryParameters query)
        {
            var result = await _employeeRepository.GetEmployeesAsync(query);
            return Ok(result);
        }

        // ==========================
        // GET EMPLOYEE BY ID
        // Admin and Manager Only
        // ==========================
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound(new
                {
                    Message = "Employee not found."
                });
            }

            return Ok(employee);
        }

        // ==========================
        // CREATE EMPLOYEE
        // Admin Only
        // ==========================
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var employee = await _employeeRepository.CreateEmployeeAsync(dto);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message
                });
            }
        }

        // ==========================
        // UPDATE EMPLOYEE
        // Admin Only
        // ==========================
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var employee = await _employeeRepository.UpdateEmployeeAsync(id, dto);

                if (employee == null)
                {
                    return NotFound(new
                    {
                        Message = "Employee not found."
                    });
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message
                });
            }
        }

        // ==========================
        // DELETE EMPLOYEE
        // Admin Only
        // ==========================
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var deleted = await _employeeRepository.DeleteEmployeeAsync(id);

                if (!deleted)
                {
                    return NotFound(new
                    {
                        Message = "Employee not found."
                    });
                }

                return Ok(new
                {
                    Message = "Employee deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message
                });
            }
        }
        [HttpGet("ping")]
public IActionResult Ping()
{
    return Ok("API is working");
}

[Authorize]
[HttpGet("secure-ping")]
public IActionResult SecurePing()
{
    return Ok(new
    {
        Name = User.Identity?.Name,
        Roles = User.Claims
            .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
            .Select(c => c.Value)
    });
}

[Authorize(Roles = "Admin")]
[HttpGet("admin-ping")]
public IActionResult AdminPing()
{
    return Ok("Admin access granted");
}
    }
}