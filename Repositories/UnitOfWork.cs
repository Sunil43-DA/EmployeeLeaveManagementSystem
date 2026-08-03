using EmployeeLeaveManagement.API.Data;
using EmployeeLeaveManagement.API.Interfaces;
using EmployeeLeaveManagement.API.Models;

namespace EmployeeLeaveManagement.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Employee> Employees { get; }

        public IGenericRepository<User> Users { get; }

        public IGenericRepository<RefreshToken> RefreshTokens { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Employees = new GenericRepository<Employee>(_context);

            Users = new GenericRepository<User>(_context);

            RefreshTokens = new GenericRepository<RefreshToken>(_context);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}