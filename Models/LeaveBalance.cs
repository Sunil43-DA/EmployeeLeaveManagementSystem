using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.API.Models;

public partial class LeaveBalance
{
    public int LeaveBalanceId { get; set; }

    public int EmployeeId { get; set; }

    public int LeaveTypeId { get; set; }

    public int LeaveYear { get; set; }

    public int TotalAllocated { get; set; }

    public int UsedLeaves { get; set; }

    public int RemainingLeaves { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual LeaveType LeaveType { get; set; } = null!;
}
