using EmployeeLeaveManagement.API.DTOs;

namespace EmployeeLeaveManagement.API.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);

        Task<bool> RegisterAsync(RegisterDto registerDto);

        Task<LoginResponseDto?> RefreshTokenAsync(
            RefreshTokenRequestDto request);
        Task<bool> LogoutAsync(LogoutRequestDto request);
    }
}