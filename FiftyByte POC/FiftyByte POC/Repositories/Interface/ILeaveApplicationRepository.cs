using FiftyByte_POC.Models.leave_management;

namespace FiftyByte_POC.Repositories.Interface
{
    public interface ILeaveApplicationRepository
    {
        Task<int> ApplyLeaveAsync(LeaveApplication leaveApplication);
        Task<IEnumerable<LeavePolicy>> GetLeavePoliciesAsync();
        Task<IEnumerable<CostCenter>> GetCostCentersAsync();
    }

}
