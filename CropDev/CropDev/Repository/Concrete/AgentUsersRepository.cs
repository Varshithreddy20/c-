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

        public async Task<AgentUsers> GetById(int agentUserId)
        {
            AgentUsers agentUsers = null;

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
                        agentUsers = new AgentUsers
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

        public async Task<ResultStatus> Update(AgentUsers agentUsers)
        {
            try
            {
                using var sqlConnection = new SqlConnection(_appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[UpdateAgentUsers]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@AgentUserId", SqlDbType.Int) { Value = agentUsers.AgentUserId });
                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 100) { Value = agentUsers.FirstName });
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 100) { Value = agentUsers.LastName });
                sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 100) { Value = agentUsers.City });
                sqlCommand.Parameters.Add(new SqlParameter("@State", SqlDbType.VarChar, 50) { Value = agentUsers.State });
                sqlCommand.Parameters.Add(new SqlParameter("@ZipCode", SqlDbType.Int) { Value = agentUsers.ZipCode });
                sqlCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50) { Value = agentUsers.Country });
                sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 20) { Value = agentUsers.PhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@SecondaryPhoneNumber", SqlDbType.VarChar, 20) { Value = agentUsers.SecondaryPhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar, 50) { Value = agentUsers.UpdatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating agent user with id {agentUsers.AgentUserId}");
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Create(AgentUsers agentUsers)
        {
            try
            {
                using var sqlConnection = new SqlConnection(_appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[CreateAgentUsers]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = agentUsers.FirstName });
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = agentUsers.LastName });
                sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 100) { Value = agentUsers.City });
                sqlCommand.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar, 50) { Value = agentUsers.State });
                sqlCommand.Parameters.Add(new SqlParameter("@ZipCode", SqlDbType.Int) { Value = agentUsers.ZipCode });
                sqlCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar, 50) { Value = agentUsers.Country });
                sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 20) { Value = agentUsers.PhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@SecondaryPhoneNumber", SqlDbType.NVarChar, 20) { Value = agentUsers.SecondaryPhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50) { Value = agentUsers.CreatedBy });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar, 50) { Value = agentUsers.UpdatedBy });

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

        public async Task<List<AgentUsers>> GetAll()
        {
            List<AgentUsers> agentUsersList = new List<AgentUsers>();

            try
            {
                using var sqlConnection = new SqlConnection(_appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[GetAllAgentUsers]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                await sqlConnection.OpenAsync();

                using var reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    AgentUsers agentUser = new AgentUsers
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
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : null,
                        UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"])
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
