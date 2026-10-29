
using LeaveManagementEFCore.Data;
using LeaveManagementEFCore.Models;
using LeaveManagementEFCore.Repositories;
using LeaveManagementEFCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Server=.\\SQLEXPRESS;Database=LeaveManagementEFCoreDB;Trusted_Connection=True;TrustServerCertificate=True;"));

services.AddScoped<EmployeeRepository>();
services.AddScoped<EmployeeService>();

services.AddScoped<LeaveTypeRepository>();
services.AddScoped<LeaveTypeService>();

services.AddScoped<LeaveRequestRepository>();
services.AddScoped<LeaveRequestService>();

var serviceProvider = services.BuildServiceProvider();

bool exit = false;

while (!exit)
{
    Console.WriteLine("\n===== LEAVE MANAGEMENT SYSTEM =====");
    Console.WriteLine("1. Employee Management");
    Console.WriteLine("2. Leave Type Management");
    Console.WriteLine("3. Leave Request Management");
    Console.WriteLine("4. Exit");
    Console.Write("Enter your choice: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            EmployeeMenu();
            break;

        case "2":
            LeaveTypeMenu();
            break;

        case "3":
            LeaveRequestMenu();
            break;

        case "4":
            exit = true;
            Console.WriteLine("Exiting Leave Management System...");
            break;

        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

void EmployeeMenu()
{
    bool back = false;

    while (!back)
    {
        Console.WriteLine("\n===== EMPLOYEE MANAGEMENT =====");
        Console.WriteLine("1. View All Employees");
        Console.WriteLine("2. Add Employee");
        Console.WriteLine("3. Back to Main Menu");
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        using var scope = serviceProvider.CreateScope();
        var employeeService =
            scope.ServiceProvider.GetRequiredService<EmployeeService>();

        switch (choice)
        {
            case "1":
                var employees = employeeService.GetAllEmployees();

                if (employees.Count == 0)
                {
                    Console.WriteLine("No employees found.");
                    break;
                }

                foreach (var employee in employees)
                {
                    Console.WriteLine(
                        $"ID: {employee.EmployeeId}, " +
                        $"Name: {employee.EmployeeName}, " +
                        $"Email: {employee.Email}, " +
                        $"Department: {employee.Department}, " +
                        $"Active: {employee.IsActive}");
                }
                break;

            case "2":
                Console.Write("Enter employee name: ");
                string employeeName = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter email: ");
                string email = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter department: ");
                string department = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(employeeName) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(department))
                {
                    Console.WriteLine("All fields are required.");
                    break;
                }

                var newEmployee = new Employee
                {
                    EmployeeName = employeeName,
                    Email = email,
                    Department = department,
                    IsActive = true
                };

                employeeService.AddEmployee(newEmployee);
                Console.WriteLine("Employee added successfully.");
                break;

            case "3":
                back = true;
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}

void LeaveTypeMenu()
{
    bool back = false;

    while (!back)
    {
        Console.WriteLine("\n===== LEAVE TYPE MANAGEMENT =====");
        Console.WriteLine("1. View All Leave Types");
        Console.WriteLine("2. Add Leave Type");
        Console.WriteLine("3. Back to Main Menu");
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        using var scope = serviceProvider.CreateScope();
        var leaveTypeService =
            scope.ServiceProvider.GetRequiredService<LeaveTypeService>();

        switch (choice)
        {
            case "1":
                var leaveTypes = leaveTypeService.GetAllLeaveTypes();

                if (leaveTypes.Count == 0)
                {
                    Console.WriteLine("No leave types found.");
                    break;
                }

                foreach (var leaveType in leaveTypes)
                {
                    Console.WriteLine(
                        $"ID: {leaveType.LeaveTypeId}, " +
                        $"Name: {leaveType.LeaveTypeName}, " +
                        $"Days Allowed: {leaveType.DefaultDaysAllowed}, " +
                        $"Active: {leaveType.IsActive}");
                }
                break;

            case "2":
                Console.Write("Enter leave type name: ");
                string leaveTypeName = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter default days allowed: ");
                string daysInput = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(leaveTypeName) ||
                    !int.TryParse(daysInput, out int daysAllowed) ||
                    daysAllowed < 0)
                {
                    Console.WriteLine("Enter a valid name and non-negative number of days.");
                    break;
                }

                var newLeaveType = new LeaveType
                {
                    LeaveTypeName = leaveTypeName,
                    DefaultDaysAllowed = daysAllowed,
                    IsActive = true
                };

                leaveTypeService.AddLeaveType(newLeaveType);
                Console.WriteLine("Leave type added successfully.");
                break;

            case "3":
                back = true;
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}

void LeaveRequestMenu()
{
    bool back = false;

    while (!back)
    {
        Console.WriteLine("\n===== LEAVE REQUEST MANAGEMENT =====");
        Console.WriteLine("1. View All Leave Requests");
        Console.WriteLine("2. Apply for Leave");
        Console.WriteLine("3. Back to Main Menu");
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        using var scope = serviceProvider.CreateScope();

        var leaveRequestService =
            scope.ServiceProvider.GetRequiredService<LeaveRequestService>();

        switch (choice)
        {
            case "1":
                var requests = leaveRequestService.GetAllLeaveRequests();

                if (requests.Count == 0)
                {
                    Console.WriteLine("No leave requests found.");
                    break;
                }

                foreach (var request in requests)
                {
                    Console.WriteLine(
                        $"Request ID: {request.LeaveRequestId}, " +
                        $"Employee: {request.Employee.EmployeeName}, " +
                        $"Leave Type: {request.LeaveType.LeaveTypeName}, " +
                        $"Start: {request.StartDate:yyyy-MM-dd}, " +
                        $"End: {request.EndDate:yyyy-MM-dd}, " +
                        $"Status: {request.Status}, " +
                        $"Reason: {request.Reason}");
                }
                break;

            case "2":
                var employees = scope.ServiceProvider
                    .GetRequiredService<EmployeeService>()
                    .GetAllEmployees();

                if (employees.Count == 0)
                {
                    Console.WriteLine("Add an employee before applying for leave.");
                    break;
                }

                Console.WriteLine("\nAvailable Employees:");
                foreach (var employee in employees)
                {
                    Console.WriteLine(
                        $"{employee.EmployeeId}. {employee.EmployeeName}");
                }

                Console.Write("Enter employee ID: ");
                if (!int.TryParse(Console.ReadLine(), out int employeeId) ||
                    !employees.Any(e => e.EmployeeId == employeeId))
                {
                    Console.WriteLine("Invalid employee ID.");
                    break;
                }

                var leaveTypes = scope.ServiceProvider
                    .GetRequiredService<LeaveTypeService>()
                    .GetAllLeaveTypes();

                if (leaveTypes.Count == 0)
                {
                    Console.WriteLine("Add a leave type before applying for leave.");
                    break;
                }

                Console.WriteLine("\nAvailable Leave Types:");
                foreach (var leaveType in leaveTypes)
                {
                    Console.WriteLine(
                        $"{leaveType.LeaveTypeId}. {leaveType.LeaveTypeName} " +
                        $"({leaveType.DefaultDaysAllowed} days allowed)");
                }

                Console.Write("Enter leave type ID: ");
                if (!int.TryParse(Console.ReadLine(), out int leaveTypeId) ||
                    !leaveTypes.Any(lt => lt.LeaveTypeId == leaveTypeId))
                {
                    Console.WriteLine("Invalid leave type ID.");
                    break;
                }

                Console.Write("Enter start date (yyyy-MM-dd): ");
                if (!DateTime.TryParseExact(
                        Console.ReadLine(),
                        "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime startDate))
                {
                    Console.WriteLine("Invalid start date format.");
                    break;
                }

                Console.Write("Enter end date (yyyy-MM-dd): ");
                if (!DateTime.TryParseExact(
                        Console.ReadLine(),
                        "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime endDate) ||
                    endDate < startDate)
                {
                    Console.WriteLine("Invalid end date. It must be on or after the start date.");
                    break;
                }

                Console.Write("Enter reason for leave: ");
                string reason = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(reason))
                {
                    Console.WriteLine("Reason is required.");
                    break;
                }

                var newRequest = new LeaveRequest
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveTypeId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Reason = reason,
                    Status = "Pending"
                };

                leaveRequestService.AddLeaveRequest(newRequest);
                Console.WriteLine("Leave request submitted successfully. Status: Pending.");
                break;

            case "3":
                back = true;
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}
