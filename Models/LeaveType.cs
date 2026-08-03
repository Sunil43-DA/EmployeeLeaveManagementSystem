using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.API.Models;

public partial class LeaveType
{
    public int LeaveTypeId { get; set; }

    public string LeaveName { get; set; } = null!;

    public int MaxDays { get; set; }

    public bool CarryForward { get; set; }

    public bool IsPaidLeave { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
