
using LeaveManagementEFCore.Data;
using LeaveManagementEFCore.Models;

namespace LeaveManagementEFCore.Repositories;

public class LeaveTypeRepository
{
    private readonly AppDbContext _context;

    public LeaveTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<LeaveType> GetAllLeaveTypes()
    {
        return _context.LeaveTypes.ToList();
    }

    public LeaveType? GetLeaveTypeById(int leaveTypeId)
    {
        return _context.LeaveTypes
            .FirstOrDefault(l => l.LeaveTypeId == leaveTypeId);
    }

    public void AddLeaveType(LeaveType leaveType)
    {
        _context.LeaveTypes.Add(leaveType);
        _context.SaveChanges();
    }

    public void UpdateLeaveType(LeaveType leaveType)
    {
        _context.LeaveTypes.Update(leaveType);
        _context.SaveChanges();
    }

    public void DeleteLeaveType(int leaveTypeId)
    {
        var leaveType = GetLeaveTypeById(leaveTypeId);

        if (leaveType != null)
        {
            _context.LeaveTypes.Remove(leaveType);
            _context.SaveChanges();
        }
    }
}
