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
    public class AgentUsersController(IAgentUsersService agentUsersService) : ControllerBase
    {
        /// <summary>
        /// Gets all AgentUsers details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agentUsers = await agentUsersService.GetAll();
            return Ok(agentUsers);
        }
        /// <summary>
        ///  Creates Agent Users
        /// </summary>
        /// <param name="createAgentUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAgentUser createAgentUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await agentUsersService.Create(createAgentUser);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.DuplicateEntry => Conflict("Agent already exists."),
                _ => BadRequest("Unable to create the agent."),
            };
        }
        /// <summary>
        /// updates Agent Users
        /// </summary>
        /// <param name="updateAgentUser"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAgentUser updateAgentUser)
        {
            if (!ModelState.IsValid || updateAgentUser.AgentUserId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await agentUsersService.Update(updateAgentUser);

            return result == ResultStatus.Success ? Ok(result)
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update Agent");
        }
        /// <summary>
        /// Gets AgentUser by Id
        /// </summary>
        /// <param name="agentUserId"></param>
        /// <returns></returns>
        [HttpGet("GetAgent/{agentUserId}")]
        public async Task<IActionResult> GetById(int agentUserId)
        {
            var result = await agentUsersService.GetById(agentUserId);

            return (result != null && result.AgentUserId != 0) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }
        /// <summary>
        /// Deletes Agent User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] string updatedBy)
        {
            var result = await agentUsersService.SoftDelete(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("AgentUser is soft deleted successfully.");
            }
            return BadRequest("Failed to soft delete the AgentUser.");
        }
        /// <summary>
        /// Restores Agent User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Restore(int id, [FromQuery] string updatedBy)
        {
            var result = await agentUsersService.Restore(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("AgentUser restored successfully.");
            }
            return BadRequest("Failed to restore the AgentUser.");
        }
    }
}
