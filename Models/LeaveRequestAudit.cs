using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.API.Models;

public partial class LeaveRequestAudit
{
    public int AuditId { get; set; }

    public int? LeaveRequestId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Action { get; set; }

    public DateTime? AuditDate { get; set; }
}
