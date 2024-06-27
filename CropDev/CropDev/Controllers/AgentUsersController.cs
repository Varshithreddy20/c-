using CropDev.Models;
using CropDev.Service.Concrete;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet("GetAllAgents")]
        public async Task<IActionResult> GetAll()
        {
            var agentUsers = await _agentUsersService.GetAll();
            return Ok(agentUsers);
        }

        [HttpPost("CreateAgent")]
        public async Task<IActionResult> Create([FromBody] AgentUsers agentUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _agentUsersService.Create(agentUsers);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.DuplicateEntry => Conflict("Agent already exists."),
                _ => BadRequest("Unable to create the agent."),
            };
        }
        [HttpPut("UpdateAgent")]
        public async Task<IActionResult> Update([FromBody] AgentUsers agentUsers)
        {
            if (!ModelState.IsValid || agentUsers.AgentUserId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _agentUsersService.Update(agentUsers);

            return result == ResultStatus.Success ? Ok(result)
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update Agent");
        }

        [HttpGet("GetAgent/{agentUserId}")]
        public async Task<IActionResult> GetById(int agentUserId)
        {
            var result = await _agentUsersService.GetById(agentUserId);

            return (result != null && result.AgentUserId != 0) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }
    }
}
