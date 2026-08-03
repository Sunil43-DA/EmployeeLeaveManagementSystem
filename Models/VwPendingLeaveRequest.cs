using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.API.Models;

public partial class VwPendingLeaveRequest
{
    public int LeaveRequestId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string LeaveName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int TotalDays { get; set; }

    public DateTime AppliedDate { get; set; }
}
