using CropDev.Models;
using CropDev.Utilities.Enums;
using CropDev.Models.AgentUsers;


namespace CropDev.Repository.Interface
{
    public interface IAgentUsersRepository 
    {

        /// <summary>
        /// Retrieves a list of all AgentUser objects.
        /// </summary>
        /// <returns>A list of AgentUser objects</returns>
        Task<List<AgentUser>> GetAll();
        /// <summary>
        /// Creates a new Agent User
        /// </summary>
        /// <param name="createAgentUser"></param>
        /// <returns>created agent user if created shows 1</returns>
        Task<ResultStatus> Create(CreateAgentUser createAgentUser);
        /// <summary>
        /// Updates a existing AgentUser 
        /// </summary>
        /// <param name="updateAgentUser"></param>
        /// <returns>Shoes one if it is  Updated</returns>
        Task<ResultStatus> Update(UpdateAgentUser updateAgentUser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentUserId"></param>
        /// <returns></returns>
        Task<AgentUser> GetById(int agentUserId);
        /// <summary>
        /// Retrieves AgentUser By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns>A Agentuser Id is retrived</returns>
        Task<ResultStatus> SoftDelete(int id, string updatedBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<ResultStatus> Restore(int id, string updatedBy);
        
    }
}
