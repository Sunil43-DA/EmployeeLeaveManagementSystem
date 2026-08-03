using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.API.Models;

public partial class EmployeeSalaryAudit
{
    public int AuditId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? OldSalary { get; set; }

    public decimal? NewSalary { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
