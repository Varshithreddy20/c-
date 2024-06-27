
using CropDev.Models;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDev.Service.Interface
{
    public interface IAgentUsersService
    {
        //Task<ResultStatus> SoftDelete(int agentUserId, string updatedBy);
        //Task<ResultStatus> Restore(int agentUserId, string updatedBy);
        Task<AgentUsers> GetById(int agentUserId);
        Task<ResultStatus> Update(AgentUsers agentUsers);
        Task<ResultStatus> Create(AgentUsers agentUsers);
        Task<List<AgentUsers>> GetAll();
    }
}
