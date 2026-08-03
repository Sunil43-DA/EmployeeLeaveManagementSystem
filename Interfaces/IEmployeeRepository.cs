using EmployeeLeaveManagement.API.DTOs;
using EmployeeLeaveManagement.API.Helpers;

namespace EmployeeLeaveManagement.API.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<PagedResult<EmployeeDto>> GetEmployeesAsync(EmployeeQueryParameters query);

        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);

        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);

        Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}