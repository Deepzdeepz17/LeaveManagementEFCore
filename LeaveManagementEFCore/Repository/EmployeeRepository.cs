
using LeaveManagementEFCore.Data;
using LeaveManagementEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementEFCore.Repositories;

public class EmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Employee> GetAllEmployees()
    {
        return _context.Employees.ToList();
    }

    public Employee? GetEmployeeById(int employeeId)
    {
        return _context.Employees
            .FirstOrDefault(e => e.EmployeeId == employeeId);
    }

    public void AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void UpdateEmployee(Employee employee)
    {
        _context.Employees.Update(employee);
        _context.SaveChanges();
    }

    public void DeleteEmployee(int employeeId)
    {
        var employee = GetEmployeeById(employeeId);

        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}
