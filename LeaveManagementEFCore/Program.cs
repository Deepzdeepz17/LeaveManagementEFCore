
using LeaveManagementEFCore.Data;
using LeaveManagementEFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Server=.\\SQLEXPRESS;Database=LeaveManagementEFCoreDB;Trusted_Connection=True;TrustServerCertificate=True;"));

// Register EmployeeRepository with Dependency Injection
builder.Services.AddScoped<EmployeeRepository>();

using var host = builder.Build();

Console.WriteLine("Leave Management System");
Console.WriteLine("EF Core and Dependency Injection configured.");
