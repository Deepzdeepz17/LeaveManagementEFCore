
using LeaveManagementEFCore.Models;
using LeaveManagementEFCore.Repositories;

namespace LeaveManagementEFCore.Services;


public class EmployeeService
{
    private readonly EmployeeRepository _employeeRepository;

    public EmployeeService(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public List<Employee> GetAllEmployees()
    {
        return _employeeRepository.GetAllEmployees();
    }

    public Employee? GetEmployeeById(int employeeId)
    {
        return _employeeRepository.GetEmployeeById(employeeId);
    }

    public void AddEmployee(Employee employee)
    {
        _employeeRepository.AddEmployee(employee);
    }

    public void UpdateEmployee(Employee employee)
    {
        _employeeRepository.UpdateEmployee(employee);
    }

    public void DeleteEmployee(int employeeId)
    {
        _employeeRepository.DeleteEmployee(employeeId);
    }
}
