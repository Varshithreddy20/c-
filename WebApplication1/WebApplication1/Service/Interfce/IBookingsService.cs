using CRAVENEST.Model.Bookings;
using CRAVENEST.Utilities.Eums;

namespace CRAVENEST.Service.Interfce
{
    public interface IBookingsService
    {
        /// <summary>
        /// Retrieves a list of all AgentUser objects.
        /// </summary>
        /// <returns>A list of AgentUser objects</returns>
        Task<List<Bookings>> GetAll();
        /// <summary>
        /// Creates a new Agent User
        /// </summary>
        /// <param name="createAgentUser"></param>
        /// <returns>created agent user if created shows 1</returns>
        Task<ResultStatus> Create(Bookings bookings);
        /// <summary>
        /// Updates a existing AgentUser 
        /// </summary>
        /// <param name="updateAgentUser"></param>
        /// <returns>Shoes one if it is  Updated</returns>
        Task<ResultStatus> Update(Bookings bookings);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentUserId"></param>
        /// <returns></returns>
        Task<Bookings?> GetById(int bookingsId);
        /// <summary>
        /// Retrieves AgentUser By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns>A Agentuser Id is retrived</returns>
        Task<ResultStatus> SoftDelete(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<ResultStatus> Restore(int id);
        Task<ResultStatus> UpdateStatus(int bookingId, string status);
        Task<List<Bookings>> GetBookingsBySignUpId(int signUpId);

    }
}

