using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveManagement.API.DTOs
{
    public class CreateEmployeeDto
    {
        [Required]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? JobTitle { get; set; }

        public decimal? Salary { get; set; }

        [Required]
        public DateOnly HireDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public int? ManagerId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}