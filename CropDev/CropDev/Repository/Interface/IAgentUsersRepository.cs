using CropDev.Models;
using CropDev.Utilities.Enums;
using CropDev.Models.AgentUsers;


namespace CropDev.Repository.Interface
{
    public interface IAgentUsersRepository 
    {
        Task<List<AgentUser>> GetAll();
        Task<ResultStatus> Create(CreateAgentUser createAgentUser);
        Task<ResultStatus> Update(UpdateAgentUser updateAgentUser);
        Task<AgentUser> GetById(int agentUserId);
        Task<ResultStatus> SoftDelete(int id, string updatedBy);
        Task<ResultStatus> Restore(int id, string updatedBy);
    }
}
