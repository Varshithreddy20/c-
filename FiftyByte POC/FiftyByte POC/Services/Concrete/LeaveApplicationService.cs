using FiftyByte_POC.Models.leave_management;
using FiftyByte_POC.Repositories.Interface;
using FiftyByte_POC.Services.Interface;

namespace FiftyByte_POC.Services.Concrete
{
    public class LeaveApplicationService : ILeaveApplicationService
    {
        private readonly ILeaveApplicationRepository _leaveApplicationRepository;

        public LeaveApplicationService(ILeaveApplicationRepository leaveApplicationRepository)
        {
            _leaveApplicationRepository = leaveApplicationRepository;
        }

        public async Task<int> ApplyLeaveAsync(LeaveApplication leaveApplication)
        {
            return await _leaveApplicationRepository.ApplyLeaveAsync(leaveApplication);
        }


        public async Task<IEnumerable<LeavePolicy>> GetLeavePoliciesAsync()
        {
            return await _leaveApplicationRepository.GetLeavePoliciesAsync();
        }

        public async Task<IEnumerable<CostCenter>> GetCostCentersAsync()
        {
            return await _leaveApplicationRepository.GetCostCentersAsync();
        }
    }
}
