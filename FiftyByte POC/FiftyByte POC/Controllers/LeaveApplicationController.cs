using FiftyByte_POC.Models.leave_management;
using FiftyByte_POC.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FiftyByte_POC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveApplicationController : ControllerBase
    {
        private readonly ILeaveApplicationService _leaveApplicationService;

        public LeaveApplicationController(ILeaveApplicationService leaveApplicationService)
        {
            _leaveApplicationService = leaveApplicationService;
        }

        [HttpPost("apply-leave")]
        public async Task<IActionResult> ApplyLeave([FromBody] LeaveApplication leaveApplication)
        {
            if (leaveApplication == null)
            {
                return BadRequest("Invalid leave application data.");
            }

            try
            {
                var savedLeaveApplication = await _leaveApplicationService.ApplyLeaveAsync(leaveApplication);

                if (savedLeaveApplication != null)
                {
                    return Ok(savedLeaveApplication);
                }

                return StatusCode(500, "An error occurred while applying for leave.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("leave-policies")]
        public async Task<IActionResult> GetLeavePolicies()
        {
            try
            {
                var leavePolicies = await _leaveApplicationService.GetLeavePoliciesAsync();
                return Ok(leavePolicies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("cost-centers")]
        public async Task<IActionResult> GetCostCenters()
        {
            try
            {
                var costCenters = await _leaveApplicationService.GetCostCentersAsync();
                return Ok(costCenters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
