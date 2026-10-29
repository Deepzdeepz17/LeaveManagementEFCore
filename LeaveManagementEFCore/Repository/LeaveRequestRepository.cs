
using LeaveManagementEFCore.Data;
using LeaveManagementEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementEFCore.Repositories;

public class LeaveRequestRepository
{
    private readonly AppDbContext _context;

    public LeaveRequestRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<LeaveRequest> GetAllLeaveRequests()
    {
        return _context.LeaveRequests
            .Include(lr => lr.Employee)
            .Include(lr => lr.LeaveType)
            .ToList();
    }

    public LeaveRequest? GetLeaveRequestById(int id)
    {
        return _context.LeaveRequests
            .Include(lr => lr.Employee)
            .Include(lr => lr.LeaveType)
            .FirstOrDefault(lr => lr.LeaveRequestId == id);
    }

    public void AddLeaveRequest(LeaveRequest leaveRequest)
    {
        _context.LeaveRequests.Add(leaveRequest);
        _context.SaveChanges();
    }

    public void UpdateLeaveRequest(LeaveRequest leaveRequest)
    {
        _context.LeaveRequests.Update(leaveRequest);
        _context.SaveChanges();
    }

    public void DeleteLeaveRequest(int id)
    {
        var leaveRequest = _context.LeaveRequests.Find(id);

        if (leaveRequest != null)
        {
            _context.LeaveRequests.Remove(leaveRequest);
            _context.SaveChanges();
        }
    }
}
