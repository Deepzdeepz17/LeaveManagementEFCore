
using LeaveManagementEFCore.Data;
using LeaveManagementEFCore.Models;
using LeaveManagementEFCore.Repositories;
using LeaveManagementEFCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Server=.\\SQLEXPRESS;Database=LeaveManagementEFCoreDB;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();

using var host = builder.Build();

Console.WriteLine("Leave Management System");

using var scope = host.Services.CreateScope();
var employeeService = scope.ServiceProvider.GetRequiredService<EmployeeService>();

bool running = true;

while (running)
{
    Console.WriteLine("\n--- Employee Management ---");
    Console.WriteLine("1. View All Employees");
    Console.WriteLine("2. Add Employee");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            var employees = employeeService.GetAllEmployees();

            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
            }
            else
            {
                foreach (var employee in employees)
                {
                    Console.WriteLine(
                        $"ID: {employee.EmployeeId}, " +
                        $"Name: {employee.EmployeeName}, " +
                        $"Email: {employee.Email}, " +
                        $"Department: {employee.Department}, " +
                        $"Active: {employee.IsActive}");
                }
            }
            break;

        case "2":
            Console.Write("Enter employee name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Enter department: ");
            string department = Console.ReadLine() ?? "";

            var newEmployee = new Employee
            {
                EmployeeName = name,
                Email = email,
                Department = department,
                IsActive = true
            };

            employeeService.AddEmployee(newEmployee);
            Console.WriteLine("Employee added successfully.");
            break;

        case "0":
            running = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }
}
