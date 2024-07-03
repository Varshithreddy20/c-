using CropDev.Repository.Interface;
using CropDev.Service.Interface;
using CropDev.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Utilities.Enums;
using CropDev.Models.AgentUsers;

namespace CropDev.Service.Concrete
{
    public class AgentUsersService(IAgentUsersRepository agentUsersRepository) : IAgentUsersService
    {
        public async Task<List<AgentUser>> GetAll()
        {
            return await agentUsersRepository.GetAll();
        }

        public async Task<ResultStatus> Create(CreateAgentUser createAgentUser)
        {
            return await agentUsersRepository.Create(createAgentUser);
        }

        public async Task<ResultStatus> Update(UpdateAgentUser updateAgentUser)
        {
            return await agentUsersRepository.Update(updateAgentUser);
        }

        public async Task<AgentUser> GetById(int agentUserId)
        {
            return await agentUsersRepository.GetById(agentUserId);
        }

        public async Task<ResultStatus> SoftDelete(int agentUserId, string updatedBy)
        {
            return await agentUsersRepository.SoftDelete(agentUserId, updatedBy);
        }

        public async Task<ResultStatus> Restore(int agentUserId, string updatedBy)
        {
            return await agentUsersRepository.Restore(agentUserId, updatedBy);
        }
    }
}
