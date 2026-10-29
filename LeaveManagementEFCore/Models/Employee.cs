using System;
using System.Collections.Generic;
using System.Text;


namespace LeaveManagementEFCore.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}