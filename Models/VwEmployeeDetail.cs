using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.API.Models;

public partial class VwEmployeeDetail
{
    public int EmployeeId { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? JobTitle { get; set; }

    public decimal? Salary { get; set; }

    public DateOnly HireDate { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? ManagerName { get; set; }

    public bool? IsActive { get; set; }
}
