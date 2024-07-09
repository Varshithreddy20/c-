using CropDev.Models;
using CropDev.Models.Farmers;
using CropDev.Repository.Interface;
using CropDev.Utilities;
using CropDev.Utilities.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace CropDev.Repository.Concrete
{
    public class FarmersRepository : IFarmersRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FarmersRepository> _logger;

        public FarmersRepository(IConfiguration configuration, ILogger<FarmersRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ResultStatus> SoftDelete(int farmerId, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[SoftDeleteFarmerById]", farmerId, updatedBy);
        }

        public async Task<ResultStatus> Restore(int farmerId, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[RestoreFarmerById]", farmerId, updatedBy);
        }

        private async Task<ResultStatus> ExecuteNonQuery(string storedProcedure, int farmerId, string updatedBy)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("FarmersDBConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await using var sqlCommand = new SqlCommand(storedProcedure, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerId", SqlDbType.Int) { Value = farmerId });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar) { Value = updatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error executing {storedProcedure} for FarmerId {farmerId}");
                return ResultStatus.Failed;
            }
        }

        public async Task<Farmers> GetById(int farmerId)
        {
            Farmers? farmer = null;

            try
            {
                var connectionString = _configuration.GetConnectionString("FarmersDBConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                await using var sqlCommand = new SqlCommand("[dbo].[GetFarmerById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerId", SqlDbType.Int) { Value = farmerId });

                await using var reader = await sqlCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    farmer = new Farmers
                    {
                        FarmerId = reader["FarmerId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerId"]) : 0,
                        FirstName = reader["FirstName"] != DBNull.Value ? Convert.ToString(reader["FirstName"]) : string.Empty,
                        LastName = reader["LastName"] != DBNull.Value ? Convert.ToString(reader["LastName"]) : string.Empty,
                        City = reader["City"] != DBNull.Value ? Convert.ToString(reader["City"]) : string.Empty,
                        ZipCode = reader["ZipCode"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ZipCode"]) : null,
                        Country = reader["Country"] != DBNull.Value ? Convert.ToString(reader["Country"]) : string.Empty,
                        State = reader["State"] != DBNull.Value ? Convert.ToString(reader["State"]) : string.Empty,
                        PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? Convert.ToString(reader["PhoneNumber"]) : string.Empty,
                        SecondaryPhoneNumber = reader["SecondaryPhoneNumber"] != DBNull.Value ? Convert.ToString(reader["SecondaryPhoneNumber"]) : null,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = reader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue,
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : null,
                        UpdatedOn = reader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedOn"]) : (DateTime?)null
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting farmer by id {farmerId}");
            }

            return farmer;
        }

        public async Task<ResultStatus> Update(UpdateFarmers updateFarmers)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("FarmersDBConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                await using var sqlCommand = new SqlCommand("[dbo].[UpdateFarmer]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerId", SqlDbType.Int) { Value = updateFarmers.FarmerId });
                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar) { Value = updateFarmers.FirstName });
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar) { Value = updateFarmers.LastName });
                sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar) { Value = updateFarmers.City });
                sqlCommand.Parameters.Add(new SqlParameter("@State", SqlDbType.VarChar) { Value = updateFarmers.State });
                sqlCommand.Parameters.Add(new SqlParameter("@ZipCode", SqlDbType.Int) { Value = updateFarmers.ZipCode });
                sqlCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar) { Value = updateFarmers.Country });
                sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar) { Value = updateFarmers.PhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@SecondaryPhoneNumber", SqlDbType.VarChar) { Value = updateFarmers.SecondaryPhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar) { Value = updateFarmers.UpdatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating farmer with id {updateFarmers.FarmerId}");
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Create(CreateFarmers createFarmers)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("FarmersDBConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                await using var sqlCommand = new SqlCommand("[dbo].[CreateFarmer]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = createFarmers.FirstName });
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = createFarmers.LastName });
                sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 100) { Value = createFarmers.City });
                sqlCommand.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar, 50) { Value = createFarmers.State });
                sqlCommand.Parameters.Add(new SqlParameter("@ZipCode", SqlDbType.Int) { Value = createFarmers.ZipCode });
                sqlCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar, 50) { Value = createFarmers.Country });
                sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 20) { Value = createFarmers.PhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@SecondaryPhoneNumber", SqlDbType.NVarChar, 20) { Value = createFarmers.SecondaryPhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50) { Value = createFarmers.CreatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating farmer");
                return ResultStatus.Failed;
            }
        }

        public async Task<List<Farmers>> GetAll()
        {
            var farmers = new List<Farmers>();

            try
            {
                var connectionString = _configuration.GetConnectionString("FarmersDBConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                await using var sqlCommand = new SqlCommand("[dbo].[GetAllFarmers]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await using var reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var farmer = new Farmers
                    {
                        FarmerId = reader["FarmerId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerId"]) : 0,
                        FirstName = reader["FirstName"] != DBNull.Value ? Convert.ToString(reader["FirstName"]) : string.Empty,
                        LastName = reader["LastName"] != DBNull.Value ? Convert.ToString(reader["LastName"]) : string.Empty,
                        City = reader["City"] != DBNull.Value ? Convert.ToString(reader["City"]) : string.Empty,
                        ZipCode = reader["ZipCode"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ZipCode"]) : null,
                        Country = reader["Country"] != DBNull.Value ? Convert.ToString(reader["Country"]) : string.Empty,
                        State = reader["State"] != DBNull.Value ? Convert.ToString(reader["State"]) : string.Empty,
                        PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? Convert.ToString(reader["PhoneNumber"]) : string.Empty,
                        SecondaryPhoneNumber = reader["SecondaryPhoneNumber"] != DBNull.Value ? Convert.ToString(reader["SecondaryPhoneNumber"]) : null,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = reader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue,
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : null,
                        UpdatedOn = reader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedOn"]) : (DateTime?)null
                    };

                    farmers.Add(farmer);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all farmers");
            }

            return farmers;
        }
    }
}
