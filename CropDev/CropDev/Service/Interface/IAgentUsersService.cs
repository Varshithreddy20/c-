
using CropDev.Models;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.AgentUsers;

namespace CropDev.Service.Interface
{
    public interface IAgentUsersService
    {
        Task<List<AgentUser>> GetAll();
        Task<ResultStatus> Create(CreateAgentUser createAgentUser);
        Task<ResultStatus> Update(UpdateAgentUser updateAgentUser);
        Task<AgentUser> GetById(int agentUserId);
        Task<ResultStatus> SoftDelete(int Id, string updatedBy);
        Task<ResultStatus> Restore(int Id, string updatedBy);
    }
}
