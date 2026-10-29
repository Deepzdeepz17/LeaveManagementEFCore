
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
        leaveRequest.Status = "Pending";
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

    public void ApproveLeaveRequest(int id)
    {
        var leaveRequest = _repository.GetLeaveRequestById(id);

        if (leaveRequest == null)
        {
            Console.WriteLine("Leave request not found.");
            return;
        }

        if (leaveRequest.Status != "Pending")
        {
            Console.WriteLine("Only pending leave requests can be approved.");
            return;
        }

        leaveRequest.Status = "Approved";
        _repository.UpdateLeaveRequest(leaveRequest);

        Console.WriteLine("Leave request approved successfully.");
    }

    public void RejectLeaveRequest(int id)
    {
        var leaveRequest = _repository.GetLeaveRequestById(id);

        if (leaveRequest == null)
        {
            Console.WriteLine("Leave request not found.");
            return;
        }

        if (leaveRequest.Status != "Pending")
        {
            Console.WriteLine("Only pending leave requests can be rejected.");
            return;
        }

        leaveRequest.Status = "Rejected";
        _repository.UpdateLeaveRequest(leaveRequest);

        Console.WriteLine("Leave request rejected successfully.");
    }
}
