using EmployeeLeaveManagement.API.DTOs;
using EmployeeLeaveManagement.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthRepository authRepository,
            ILogger<AuthController> logger)
        {
            _authRepository = authRepository;
            _logger = logger;
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            _logger.LogInformation(
                "Login attempt for user: {Username}",
                loginDto.Username);

            var result = await _authRepository.LoginAsync(loginDto);

            if (result == null)
            {
                _logger.LogWarning(
                    "Login failed for user: {Username}",
                    loginDto.Username);

                return Unauthorized(new
                {
                    Message = "Invalid username or password."
                });
            }

            _logger.LogInformation(
                "User {Username} logged in successfully.",
                loginDto.Username);

            return Ok(result);
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            _logger.LogInformation(
                "Registration attempt for user: {Username}",
                registerDto.Username);

            var result = await _authRepository.RegisterAsync(registerDto);

            if (!result)
            {
                _logger.LogWarning(
                    "Registration failed. Username {Username} already exists.",
                    registerDto.Username);

                return BadRequest("Username already exists.");
            }

            _logger.LogInformation(
                "User {Username} registered successfully.",
                registerDto.Username);

            return Ok("User registered successfully.");
        }

        // REFRESH TOKEN
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            RefreshTokenRequestDto request)
        {
            var result = await _authRepository.RefreshTokenAsync(request);

            if (result == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid or expired refresh token."
                });
            }

            return Ok(result);
        }
        [HttpPost("logout")]
public async Task<IActionResult> Logout(LogoutRequestDto request)
{
    var result = await _authRepository.LogoutAsync(request);

    if (!result)
    {
        return BadRequest(new
        {
            Message = "Invalid refresh token."
        });
    }

    return Ok(new
    {
        Message = "Logged out successfully."
    });
}
    }
}