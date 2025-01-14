﻿using CropDev.Models.FarmerRequest;
using CropDev.Repository.Interface;
using CropDev.Utilities.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CropDev.Repository.Concrete
{
    public class FarmerRequestRepository : IFarmerRequestRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FarmerRequestRepository> _logger;

        public FarmerRequestRepository(IConfiguration configuration, ILogger<FarmerRequestRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ResultStatus> SoftDelete(int farmerRequestId, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[SoftDeleteFarmerRequestById]", farmerRequestId, updatedBy);
        }

        public async Task<ResultStatus> Restore(int farmerRequestId, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[RestoreFarmerRequestById]", farmerRequestId, updatedBy);
        }

        private async Task<ResultStatus> ExecuteNonQuery(string storedProcedure, int farmerRequestId, string updatedBy)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await using var sqlCommand = new SqlCommand(storedProcedure, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerRequestId", SqlDbType.Int) { Value = farmerRequestId });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar) { Value = updatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync().ConfigureAwait(false);
                await sqlCommand.ExecuteNonQueryAsync().ConfigureAwait(false);

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error executing {storedProcedure} for farmerRequestId {farmerRequestId}");
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Update(UpdateFarmerRequest updateFarmerRequest)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                await using var sqlCommand = new SqlCommand("[dbo].[UpdateFarmerRequest]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@FarmerRequestId", updateFarmerRequest.FarmerRequestId);
                sqlCommand.Parameters.AddWithValue("@FarmerLandDetailsId", updateFarmerRequest.FarmerLandDetailsId);
                sqlCommand.Parameters.AddWithValue("@RequestQuery", updateFarmerRequest.RequestQuery ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AgentUserId", updateFarmerRequest.AgentUserId);
                sqlCommand.Parameters.AddWithValue("@StatusId", updateFarmerRequest.StatusId);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", updateFarmerRequest.UpdatedBy);

                var statusParameter = new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output };
                sqlCommand.Parameters.Add(statusParameter);

                await sqlCommand.ExecuteNonQueryAsync();

                int status = Convert.ToInt32(sqlCommand.Parameters["@StatusOutPut"].Value);

                return status == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating FarmerRequest with ID: {FarmerRequestId}", updateFarmerRequest.FarmerRequestId);
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Create(CreateFarmerRequest createFarmerRequest)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                await using var sqlCommand = new SqlCommand("[dbo].[CreateFarmerRequest]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@FarmerLandDetailsId", createFarmerRequest.FarmerLandDetailsId);
                sqlCommand.Parameters.AddWithValue("@RequestQuery", createFarmerRequest.RequestQuery ?? (object)DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AgentUserId", createFarmerRequest.AgentUserId);
                sqlCommand.Parameters.AddWithValue("@StatusId", createFarmerRequest.StatusId);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", createFarmerRequest.CreatedBy);

                await sqlCommand.ExecuteNonQueryAsync();

                return ResultStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating FarmerRequest with FarmerLandDetailsId: {FarmerLandDetailsId}", createFarmerRequest.FarmerLandDetailsId);
                return ResultStatus.Failed;
            }
        }

        public async Task<List<FarmerRequest>> GetAll()
        {
            var farmerRequests = new List<FarmerRequest>();

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await using var sqlCommand = new SqlCommand("[dbo].[GetAllFarmerRequests]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await sqlConnection.OpenAsync();

                await using var reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var farmerRequest = new FarmerRequest
                    {
                        FarmerRequestId = reader["FarmerRequestId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerRequestId"]) : (int?)null,
                        FarmerLandDetailsId = reader["FarmerLandDetailsId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerLandDetailsId"]) : (int?)null,
                        RequestQuery = reader["RequestQuery"] != DBNull.Value ? Convert.ToString(reader["RequestQuery"]) : string.Empty,
                        AgentUserId = reader["AgentUserId"] != DBNull.Value ? Convert.ToInt32(reader["AgentUserId"]) : (int?)null,
                        StatusId = reader["StatusId"] != DBNull.Value ? Convert.ToByte(reader["StatusId"]) : (byte?)null,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : string.Empty,
                        UpdatedOn = reader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedOn"]) : (DateTime?)null,
                    };

                    farmerRequests.Add(farmerRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all farmer requests");
            }

            return farmerRequests;
        }

        public async Task<FarmerRequest> GetById(int farmerRequestId)
        {
            FarmerRequest? farmerRequest = null;

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                await using var sqlCommand = new SqlCommand("[dbo].[GetFarmerRequestById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@FarmerRequestId", farmerRequestId);

                await using var reader = await sqlCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    farmerRequest = new FarmerRequest
                    {
                        FarmerRequestId = reader["FarmerRequestId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerRequestId"]) : (int?)null,
                        FarmerLandDetailsId = reader["FarmerLandDetailsId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerLandDetailsId"]) : (int?)null,
                        RequestQuery = reader["RequestQuery"] != DBNull.Value ? Convert.ToString(reader["RequestQuery"]) : string.Empty,
                        AgentUserId = reader["AgentUserId"] != DBNull.Value ? Convert.ToInt32(reader["AgentUserId"]) : (int?)null,
                        StatusId = reader["StatusId"] != DBNull.Value ? Convert.ToByte(reader["StatusId"]) : (byte?)null,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : string.Empty,
                        UpdatedOn = reader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedOn"]) : (DateTime?)null,
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting farmer request by ID: {FarmerRequestId}", farmerRequestId);
            }

            return farmerRequest;
        }
    }
}
