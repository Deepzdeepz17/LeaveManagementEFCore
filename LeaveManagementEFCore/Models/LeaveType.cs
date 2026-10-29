
namespace LeaveManagementEFCore.Models;

public class LeaveType
{
    public int LeaveTypeId { get; set; }

    public string LeaveTypeName { get; set; } = string.Empty;

    public int DefaultDaysAllowed { get; set; }

    public bool IsActive { get; set; } = true;
}
