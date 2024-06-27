using CropDev.Repository.Interface;
using CropDev.Service.Interface;
using CropDev.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Utilities.Enums;
using CropDev.Repository.Concrete;

namespace CropDev.Service.Concrete
{
    public class AgentUsersService : IAgentUsersService
    {
        private readonly IAgentUsersRepository _agentUsersRepository;

        public AgentUsersService(IAgentUsersRepository agentUsersRepository)
        {
            _agentUsersRepository = agentUsersRepository;
        }

        public async Task<List<AgentUsers>> GetAll()
        {
            return await _agentUsersRepository.GetAll();
        }

        public async Task<ResultStatus> Create(AgentUsers agentUsers)
        {
            return await _agentUsersRepository.Create(agentUsers);
        }

        public async Task<ResultStatus> Update(AgentUsers agentUsers)
        {
            return await _agentUsersRepository.Update(agentUsers);
        }
        public async Task<AgentUsers> GetById(int agentUserId)
        {
            return await _agentUsersRepository.GetById(agentUserId);
        }
    }
}
