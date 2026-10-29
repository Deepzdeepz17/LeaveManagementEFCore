
using LeaveManagementEFCore.Models;
using LeaveManagementEFCore.Repositories;

namespace LeaveManagementEFCore.Services;

public class LeaveTypeService
{
    private readonly LeaveTypeRepository _leaveTypeRepository;

    public LeaveTypeService(LeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

    public List<LeaveType> GetAllLeaveTypes()
    {
        return _leaveTypeRepository.GetAllLeaveTypes();
    }

    public LeaveType? GetLeaveTypeById(int leaveTypeId)
    {
        return _leaveTypeRepository.GetLeaveTypeById(leaveTypeId);
    }

    public void AddLeaveType(LeaveType leaveType)
    {
        _leaveTypeRepository.AddLeaveType(leaveType);
    }

    public void UpdateLeaveType(LeaveType leaveType)
    {
        _leaveTypeRepository.UpdateLeaveType(leaveType);
    }

    public void DeleteLeaveType(int leaveTypeId)
    {
        _leaveTypeRepository.DeleteLeaveType(leaveTypeId);
    }
}
