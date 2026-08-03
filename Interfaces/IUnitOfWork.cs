using EmployeeLeaveManagement.API.Models;

namespace EmployeeLeaveManagement.API.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> Employees { get; }

        IGenericRepository<User> Users { get; }

        IGenericRepository<RefreshToken> RefreshTokens { get; }

        Task<bool> SaveAsync();
    }
}