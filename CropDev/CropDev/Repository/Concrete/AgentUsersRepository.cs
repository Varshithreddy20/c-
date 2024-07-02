using CropDev.Repository.Interface;
using CropDev.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CropDev.Utilities;
using CropDev.Utilities.Enums;
using CropDev.Models.AgentUsers;

namespace CropDev.Repository.Concrete
{
    public class AgentUsersRepository : IAgentUsersRepository
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<AgentUsersRepository> _logger;

        public AgentUsersRepository(IOptions<AppSettings> appSettings, ILogger<AgentUsersRepository> logger)
        {
            _appSettings = appSettings;
            _logger = logger;
        }

        public async Task<ResultStatus> SoftDelete(int agentUserId, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[SoftDeleteAgentUsersById]", agentUserId, updatedBy);
        }

        public async Task<ResultStatus> Restore(int agentUserId, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[RestoreAgentUserById]", agentUserId, updatedBy);
        }

        private async Task<ResultStatus> ExecuteNonQuery(string storedProcedure, int agentUserId, string updatedBy)
        {
            try
            {
                await using var sqlConnection = new SqlConnection(_appSettings.Value.FarmersDBConnection);
                await using var sqlCommand = new SqlCommand(storedProcedure, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@AgentUserId", SqlDbType.Int) { Value = agentUserId });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar) { Value = updatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync().ConfigureAwait(false);
                await sqlCommand.ExecuteNonQueryAsync().ConfigureAwait(false);

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error executing {storedProcedure} for AgentUserId {agentUserId}");
                return ResultStatus.Failed;
            }
        }
        public async Task<AgentUser> GetById(int agentUserId)
        {
            AgentUser? agentUsers = null;

            try
            {
                using var sqlConnection = new SqlConnection(_appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[GetAgentUserById]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@AgentUserId", SqlDbType.Int) { Value = agentUserId });

                await sqlConnection.OpenAsync();

                using (var reader = await sqlCommand.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        agentUsers = new AgentUser
                        {
                            AgentUserId = reader["AgentUserId"] != DBNull.Value ? Convert.ToInt32(reader["AgentUserId"]) : 0,
                            FirstName = reader["FirstName"] != DBNull.Value ? Convert.ToString(reader["FirstName"]) : string.Empty,
                            LastName = reader["LastName"] != DBNull.Value ? Convert.ToString(reader["LastName"]) : string.Empty,
                            City = reader["City"] != DBNull.Value ? Convert.ToString(reader["City"]) : string.Empty,
                            ZipCode = reader["ZipCode"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ZipCode"]) : null,
                            Country = reader["Country"] != DBNull.Value ? Convert.ToString(reader["Country"]) : string.Empty,
                            State = reader["State"] != DBNull.Value ? Convert.ToString(reader["State"]) : string.Empty,
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? Convert.ToString(reader["PhoneNumber"]) : string.Empty,
                            SecondaryPhoneNumber = reader["SecondaryPhoneNumber"] != DBNull.Value ? Convert.ToString(reader["SecondaryPhoneNumber"]) : null,
                            CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                            CreatedOn = reader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedOn"]) : default(DateTime?),
                            UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : null,
                            UpdatedOn = reader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedOn"]) : (DateTime?)null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting agent user by id {agentUserId}");
            }

            return agentUsers;
        }

        public async Task<ResultStatus> Update(UpdateAgentUser updateAgentUser)
        {
            try
            {
                using var sqlConnection = new SqlConnection(_appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[UpdateAgentUsers]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@AgentUserId", SqlDbType.Int) { Value = updateAgentUser.AgentUserId });
                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 100) { Value = updateAgentUser.FirstName });
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 100) { Value = updateAgentUser.LastName });
                sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 100) { Value = updateAgentUser.City });
                sqlCommand.Parameters.Add(new SqlParameter("@State", SqlDbType.VarChar, 50) { Value = updateAgentUser.State });
                sqlCommand.Parameters.Add(new SqlParameter("@ZipCode", SqlDbType.Int) { Value = updateAgentUser.ZipCode });
                sqlCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50) { Value = updateAgentUser.Country });
                sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 20) { Value = updateAgentUser.PhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@SecondaryPhoneNumber", SqlDbType.VarChar, 20) { Value = updateAgentUser.SecondaryPhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar, 50) { Value = updateAgentUser.UpdatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating agent user with id {updateAgentUser.AgentUserId}");
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Create(CreateAgentUser createAgentUser)
        {
            try
            {
                using var sqlConnection = new SqlConnection(_appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[CreateAgentUsers]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = createAgentUser.FirstName });
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = createAgentUser.LastName });
                sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 100) { Value = createAgentUser.City });
                sqlCommand.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar, 50) { Value = createAgentUser.State });
                sqlCommand.Parameters.Add(new SqlParameter("@ZipCode", SqlDbType.Int) { Value = createAgentUser.ZipCode });
                sqlCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar, 50) { Value = createAgentUser.Country });
                sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 20) { Value = createAgentUser.PhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@SecondaryPhoneNumber", SqlDbType.NVarChar, 20) { Value = createAgentUser.SecondaryPhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50) { Value = createAgentUser.CreatedBy });
                

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating agent user");
                return ResultStatus.Failed;
            }
        }

        public async Task<List<AgentUser>> GetAll()
        {
            List<AgentUser> agentUsersList = new List<AgentUser>();

            try
            {
                using var sqlConnection = new SqlConnection(_appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[GetAllAgentUsers]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                await sqlConnection.OpenAsync();

                using var reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    AgentUser agentUser = new AgentUser
                    {
                        AgentUserId = reader["AgentUserId"] != DBNull.Value ? Convert.ToInt32(reader["AgentUserId"]) : 0,
                        FirstName = reader["FirstName"] != DBNull.Value ? Convert.ToString(reader["FirstName"]) : string.Empty,
                        LastName = reader["LastName"] != DBNull.Value ? Convert.ToString(reader["LastName"]) : string.Empty,
                        City = reader["City"] != DBNull.Value ? Convert.ToString(reader["City"]) : string.Empty,
                        ZipCode = reader["ZipCode"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ZipCode"]) : null,
                        Country = reader["Country"] != DBNull.Value ? Convert.ToString(reader["Country"]) : string.Empty,
                        State = reader["State"] != DBNull.Value ? Convert.ToString(reader["State"]) : string.Empty,
                        PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? Convert.ToString(reader["PhoneNumber"]) : string.Empty,
                        SecondaryPhoneNumber = reader["SecondaryPhoneNumber"] != DBNull.Value ? Convert.ToString(reader["SecondaryPhoneNumber"]) : null,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = reader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedOn"]) : (DateTime?)null,
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : null,
                        UpdatedOn = reader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedOn"]) : (DateTime?)null
                    };

                    agentUsersList.Add(agentUser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all agent users");
            }

            return agentUsersList;
        }

    }
}
