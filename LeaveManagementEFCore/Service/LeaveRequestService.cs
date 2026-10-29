
using LeaveManagementEFCore.Models;
using LeaveManagementEFCore.Repositories;

namespace LeaveManagementEFCore.Services;

public class LeaveRequestService
{
    private readonly LeaveRequestRepository _repository;

    public LeaveRequestService(LeaveRequestRepository repository)
    {
        _repository = repository;
    }

    public List<LeaveRequest> GetAllLeaveRequests()
    {
        return _repository.GetAllLeaveRequests();
    }

    public LeaveRequest? GetLeaveRequestById(int id)
    {
        return _repository.GetLeaveRequestById(id);
    }

    public void AddLeaveRequest(LeaveRequest leaveRequest)
    {
        _repository.AddLeaveRequest(leaveRequest);
    }

    public void UpdateLeaveRequest(LeaveRequest leaveRequest)
    {
        _repository.UpdateLeaveRequest(leaveRequest);
    }

    public void DeleteLeaveRequest(int id)
    {
        _repository.DeleteLeaveRequest(id);
    }
}
