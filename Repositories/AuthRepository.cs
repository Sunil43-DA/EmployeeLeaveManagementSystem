using BCrypt.Net;
using EmployeeLeaveManagement.API.Data;
using EmployeeLeaveManagement.API.DTOs;
using EmployeeLeaveManagement.API.Interfaces;
using EmployeeLeaveManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagement.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthRepository(
        IUnitOfWork unitOfWork,
        ApplicationDbContext context,
        IJwtService jwtService)
        {
        _unitOfWork = unitOfWork;
        _context = context;
        _jwtService = jwtService;
        }

        // LOGIN
        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null)
                return null;

            bool validPassword;

            if (user.PasswordHash.StartsWith("$2"))
            {
                validPassword = BCrypt.Net.BCrypt.Verify(
                    loginDto.Password,
                    user.PasswordHash);
            }
            else
            {
                validPassword = loginDto.Password == user.PasswordHash;
            }

            if (!validPassword)
                return null;

            // Generate Access Token
            var loginResponse = _jwtService.GenerateToken(
                user.Username,
                user.Role);

            // Generate Refresh Token
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save Refresh Token
            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                CreatedDate = DateTime.UtcNow,
                IsRevoked = false
            };

            _context.RefreshTokens.Add(refreshTokenEntity);

            await _context.SaveChangesAsync();

            loginResponse.RefreshToken = refreshToken;

            return loginResponse;
        }

        // REGISTER
        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == registerDto.Username);

            if (existingUser != null)
                return false;

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Role = registerDto.Role,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }

        // REFRESH TOKEN ROTATION
        public async Task<LoginResponseDto?> RefreshTokenAsync(
            RefreshTokenRequestDto request)
        {
            var storedToken = await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

            if (storedToken == null)
                return null;

            if (storedToken.IsRevoked)
                return null;

            if (storedToken.ExpiryDate < DateTime.UtcNow)
                return null;

            // Revoke old refresh token
            storedToken.IsRevoked = true;

            // Generate new Access Token
            var loginResponse = _jwtService.GenerateToken(
                storedToken.User.Username,
                storedToken.User.Role);

            // Generate new Refresh Token
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Save new Refresh Token
            var refreshTokenEntity = new RefreshToken
            {
                UserId = storedToken.UserId,
                Token = newRefreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                CreatedDate = DateTime.UtcNow,
                IsRevoked = false
            };

            _context.RefreshTokens.Add(refreshTokenEntity);

            await _context.SaveChangesAsync();

            loginResponse.RefreshToken = newRefreshToken;

            return loginResponse;
        }
        public async Task<bool> LogoutAsync(LogoutRequestDto request)
{
    var refreshToken = await _context.RefreshTokens
        .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

    if (refreshToken == null)
        return false;

    if (refreshToken.IsRevoked)
        return false;

    refreshToken.IsRevoked = true;

    await _context.SaveChangesAsync();

    return true;
}
    }

    
}