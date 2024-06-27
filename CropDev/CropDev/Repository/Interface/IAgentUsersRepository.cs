using CropDev.Models;
using CropDev.Utilities.Enums;


namespace CropDev.Repository.Interface
{
    public interface IAgentUsersRepository 
    {
        //Task<ResultStatus> SoftDelete(int agentUserId, string updatedBy);
        //Task<ResultStatus> Restore(int agentUserId, string updatedBy);
        Task<AgentUsers> GetById(int agentUserId);
        Task<ResultStatus> Update(AgentUsers agentUsers);
        Task<ResultStatus> Create(AgentUsers agentUsers);
        Task<List<AgentUsers>> GetAll();
    }
}
