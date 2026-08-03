using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.API.Models;

public partial class VwLeaveBalanceSummary
{
    public string EmployeeCode { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string LeaveName { get; set; } = null!;

    public int LeaveYear { get; set; }

    public int TotalAllocated { get; set; }

    public int UsedLeaves { get; set; }

    public int RemainingLeaves { get; set; }
}
