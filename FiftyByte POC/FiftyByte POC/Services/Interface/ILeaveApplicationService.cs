using FiftyByte_POC.Models.leave_management;

namespace FiftyByte_POC.Services.Interface
{
    public interface ILeaveApplicationService
    {
        Task<int> ApplyLeaveAsync(LeaveApplication leaveApplication);
        Task<IEnumerable<LeavePolicy>> GetLeavePoliciesAsync();
        Task<IEnumerable<CostCenter>> GetCostCentersAsync();
    }

}
