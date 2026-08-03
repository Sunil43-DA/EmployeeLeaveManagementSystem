using AutoMapper;
using EmployeeLeaveManagement.API.Data;
using EmployeeLeaveManagement.API.DTOs;
using EmployeeLeaveManagement.API.Helpers;
using EmployeeLeaveManagement.API.Interfaces;
using EmployeeLeaveManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagement.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ======================================
        // GET ALL EMPLOYEES (Pagination, Search,
        // Filter and Sorting)
        // ======================================
        public async Task<PagedResult<EmployeeDto>> GetEmployeesAsync(EmployeeQueryParameters query)
{
    query.Page = query.Page <= 0 ? 1 : query.Page;
    query.PageSize = query.PageSize <= 0 ? 10 : Math.Min(query.PageSize, 100);

    IQueryable<Employee> employees = _context.Employees
        .Include(e => e.Department);

    // Search
    if (!string.IsNullOrWhiteSpace(query.Search))
    {
        employees = employees.Where(e =>
            e.FirstName.Contains(query.Search) ||
            e.LastName.Contains(query.Search) ||
            e.EmployeeCode.Contains(query.Search));
    }

    // Filter
    if (!string.IsNullOrWhiteSpace(query.Department))
    {
        employees = employees.Where(e =>
            e.Department.DepartmentName.Contains(query.Department));
    }

    // Sorting
    switch (query.SortBy?.ToLower())
    {
        case "firstname":
            employees = query.Order.ToLower() == "desc"
                ? employees.OrderByDescending(e => e.FirstName)
                : employees.OrderBy(e => e.FirstName);
            break;

        case "lastname":
            employees = query.Order.ToLower() == "desc"
                ? employees.OrderByDescending(e => e.LastName)
                : employees.OrderBy(e => e.LastName);
            break;

        case "salary":
            employees = query.Order.ToLower() == "desc"
                ? employees.OrderByDescending(e => e.Salary)
                : employees.OrderBy(e => e.Salary);
            break;

        case "hiredate":
            employees = query.Order.ToLower() == "desc"
                ? employees.OrderByDescending(e => e.HireDate)
                : employees.OrderBy(e => e.HireDate);
            break;

        default:
            employees = employees.OrderBy(e => e.EmployeeId);
            break;
    }

    // Total count before paging
    var totalRecords = await employees.CountAsync();

    // Pagination
    var result = await employees
        .Skip((query.Page - 1) * query.PageSize)
        .Take(query.PageSize)
        .ToListAsync();

    return new PagedResult<EmployeeDto>
    {
        Page = query.Page,
        PageSize = query.PageSize,
        TotalRecords = totalRecords,
        TotalPages = (int)Math.Ceiling(totalRecords / (double)query.PageSize),
        Data = _mapper.Map<List<EmployeeDto>>(result)
    };
}

        // ======================================
        // GET EMPLOYEE BY ID
        // ======================================
        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return null;

            return _mapper.Map<EmployeeDto>(employee);
        }

        // ======================================
        // CREATE EMPLOYEE
        // ======================================
        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);

            employee.CreatedDate = DateTime.Now;

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            await _context.Entry(employee)
                .Reference(e => e.Department)
                .LoadAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

        // ======================================
        // UPDATE EMPLOYEE
        // ======================================
        public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return null;

            _mapper.Map(dto, employee);

            await _context.SaveChangesAsync();

            await _context.Entry(employee)
                .Reference(e => e.Department)
                .LoadAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

        // ======================================
        // DELETE EMPLOYEE
        // ======================================
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return false;

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}