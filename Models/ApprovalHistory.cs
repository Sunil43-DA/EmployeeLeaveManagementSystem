using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagement.API.Models;

public partial class ApprovalHistory
{
    public int ApprovalHistoryId { get; set; }

    public int LeaveRequestId { get; set; }

    public string ActionTaken { get; set; } = null!;

    public int ActionBy { get; set; }

    public DateTime ActionDate { get; set; }

    public string? Comments { get; set; }

    public virtual Employee ActionByNavigation { get; set; } = null!;

    public virtual LeaveRequest LeaveRequest { get; set; } = null!;
}
