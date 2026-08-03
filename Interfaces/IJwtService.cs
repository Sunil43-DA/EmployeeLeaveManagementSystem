using EmployeeLeaveManagement.API.DTOs;

namespace EmployeeLeaveManagement.API.Interfaces
{
    public interface IJwtService
    {
        LoginResponseDto GenerateToken(string username, string role);

        string GenerateRefreshToken();
    }
}