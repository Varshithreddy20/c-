using FiftyByte_POC.Data;
using FiftyByte_POC.Models.leave_management;
using FiftyByte_POC.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FiftyByte_POC.Repositories.Concrete
{
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public LeaveApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> ApplyLeaveAsync(LeaveApplication leaveApplication)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "ApplyLeave"; // Stored Procedure name

                 
                    command.Parameters.Add(new SqlParameter("@EmployeeId", leaveApplication.EmployeeId));
                    command.Parameters.Add(new SqlParameter("@PolicyId", leaveApplication.PolicyId));
                    command.Parameters.Add(new SqlParameter("@CostCenterId", leaveApplication.CostCenterId));
                    command.Parameters.Add(new SqlParameter("@StartDate", leaveApplication.StartDate));
                    command.Parameters.Add(new SqlParameter("@EndDate", leaveApplication.EndDate));
                    command.Parameters.Add(new SqlParameter("@Reason", leaveApplication.Reason));
                    command.Parameters.Add(new SqlParameter("@Status", leaveApplication.Status));

                    // Open the connection and execute the command
                    await _context.Database.OpenConnectionAsync();
                    var result = await command.ExecuteScalarAsync(); // Get the newly created ApplicationId

                    return Convert.ToInt32(result); // Return the ApplicationId as int
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new Exception("An error occurred while applying leave", ex);
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
        }



        public async Task<IEnumerable<LeavePolicy>> GetLeavePoliciesAsync()
        {
            try
            {
                return await _context.LeavePolicies.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching leave policies", ex);
            }
        }

        public async Task<IEnumerable<CostCenter>> GetCostCentersAsync()
        {
            try
            {
               
                return await _context.CostCenters.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching cost centers", ex);
            }
        }
    }
}
