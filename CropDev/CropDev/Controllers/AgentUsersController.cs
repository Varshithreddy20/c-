using CropDev.Models;
using CropDev.Service.Concrete;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CropDev.Models.AgentUsers;

namespace CropDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentUsersController : ControllerBase
    {
        private readonly IAgentUsersService _agentUsersService;

        public AgentUsersController(IAgentUsersService agentUsersService)
        {
            _agentUsersService = agentUsersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agentUsers = await _agentUsersService.GetAll();
            return Ok(agentUsers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAgentUser createAgentUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _agentUsersService.Create(createAgentUser);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.DuplicateEntry => Conflict("Agent already exists."),
                _ => BadRequest("Unable to create the agent."),
            };
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAgentUser updateAgentUser)
        {
            if (!ModelState.IsValid || updateAgentUser.AgentUserId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _agentUsersService.Update(updateAgentUser);

            return result == ResultStatus.Success ? Ok(result)
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update Agent");
        }

        [HttpGet("GetAgent/{agentUserId}")]
        public async Task<IActionResult> GetById(int agentUserId)
        {
            var result = await _agentUsersService.GetById(agentUserId);

            return (result != null && result.AgentUserId != 0) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }

        [HttpDelete]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] string updatedBy)
        {
            var result = await _agentUsersService.SoftDelete(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("AgentUser is soft deleted successfully.");
            }
            return BadRequest("Failed to soft delete the AgentUser.");
        }

        [HttpPatch]
        public async Task<IActionResult> Restore(int id, [FromQuery] string updatedBy)
        {
            var result = await _agentUsersService.Restore(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("AgentUser restored successfully.");
            }
            return BadRequest("Failed to restore the AgentUser.");
        }
    }
}
