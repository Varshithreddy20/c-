using CropDev.Repository.Interface;
using CropDev.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CropDev.Utilities;
using CropDev.Utilities.Enums;
using CropDev.Models.FarmerLadDetails;
using Microsoft.Extensions.Configuration;

namespace CropDev.Repository.Concrete
{
    public class FarmerLandDetailsRepository(IOptions<AppSettings> appSettings, ILogger<FarmerLandDetailsRepository> logger, IConfiguration configuration) : IFarmerLandDetailsRepository
    {
        private readonly AppSettings _appSettings = appSettings.Value;
        private readonly ILogger<FarmerLandDetailsRepository> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ResultStatus> SoftDelete(int farmerLandDetailsId, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[SoftDeleteFarmerLandDetailById]", farmerLandDetailsId, updatedBy);
        }

        public async Task<ResultStatus> Restore(int farmerLandDetailsId, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[RestoreFarmerLandDetailById]", farmerLandDetailsId, updatedBy);
        }

        private async Task<ResultStatus> ExecuteNonQuery(string storedProcedure, int farmerLandDetailsId, string updatedBy)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand(storedProcedure, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerLandDetailsId", SqlDbType.Int) { Value = farmerLandDetailsId });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar) { Value = updatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error executing {storedProcedure} for FarmerId {farmerLandDetailsId}");
                return ResultStatus.Failed;
            }
        }

        public async Task<FarmerLandDetails> GetById(int farmerLandDetailsId)
        {
            FarmerLandDetails? farmerLandDetails = null;

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[GetFarmerLandDetailById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerLandDetailsId", SqlDbType.Int) { Value = farmerLandDetailsId });

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    farmerLandDetails = new FarmerLandDetails
                    {
                        FarmerLandDetailsId = reader["FarmerLandDetailsId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerLandDetailsId"]) : (int?)null,
                        FarmerId = reader["FarmerId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerId"]) : (int?)null,
                        LandLocation = reader["LandLocation"] != DBNull.Value ? Convert.ToString(reader["LandLocation"]) : string.Empty,
                        Zipcode = reader["Zipcode"] != DBNull.Value ? Convert.ToInt32(reader["Zipcode"]) : (int?)null,
                        LandSize = reader["LandSize"] != DBNull.Value ? Convert.ToDecimal(reader["LandSize"]) : (decimal?)null,
                        IsElectricityAvailable = reader["IsElectricityAvailable"] != DBNull.Value && Convert.ToBoolean(reader["IsElectricityAvailable"]),
                        IsWaterAvailable = reader["IsWaterAvailable"] != DBNull.Value && Convert.ToBoolean(reader["IsWaterAvailable"]),
                        SoilTypeId = reader["SoilTypeId"] != DBNull.Value ? Convert.ToInt16(reader["SoilTypeId"]) : (short?)null,
                        LastCrop = reader["LastCrop"] != DBNull.Value ? Convert.ToString(reader["LastCrop"]) : string.Empty,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : string.Empty,
                        UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"]),
                        FarmerName = reader["FarmerName"] != DBNull.Value ? Convert.ToString(reader["FarmerName"]) : string.Empty,
                        SoilTypeName = reader["SoilTypeName"] != DBNull.Value ? Convert.ToString(reader["SoilTypeName"]) : string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting farmer land details by ID");
            }

            return farmerLandDetails;
        }

        public async Task<ResultStatus> Update(UpdateFarmerLandDetails updateFarmerLandDetails)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                using var sqlCommand = new SqlCommand("[dbo].[UpdateFarmerLandDetails]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerLandDetailsId", SqlDbType.Int) { Value = updateFarmerLandDetails.FarmerLandDetailsId });
                sqlCommand.Parameters.Add(new SqlParameter("@FarmerId", SqlDbType.Int) { Value = updateFarmerLandDetails.FarmerId });
                sqlCommand.Parameters.Add(new SqlParameter("@LandLocation", SqlDbType.VarChar, 50) { Value = updateFarmerLandDetails.LandLocation });
                sqlCommand.Parameters.Add(new SqlParameter("@Zipcode", SqlDbType.Int) { Value = (object)updateFarmerLandDetails.Zipcode ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@LandSize", SqlDbType.Decimal) { Value = (object)updateFarmerLandDetails.LandSize ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@IsElectricityAvailable", SqlDbType.Bit) { Value = updateFarmerLandDetails.IsElectricityAvailable });
                sqlCommand.Parameters.Add(new SqlParameter("@IsWaterAvailable", SqlDbType.Bit) { Value = updateFarmerLandDetails.IsWaterAvailable });
                sqlCommand.Parameters.Add(new SqlParameter("@SoilTypeId", SqlDbType.SmallInt) { Value = (object)updateFarmerLandDetails.SoilTypeId ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@LastCrop", SqlDbType.VarChar, 50) { Value = updateFarmerLandDetails.LastCrop });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar, 50) { Value = updateFarmerLandDetails.UpdatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating FarmerLandDetails");
                return ResultStatus.Failed;
            }
        }

        public async Task<List<FarmerLandDetails>> GetAll()
        {
            List<FarmerLandDetails> farmers = new List<FarmerLandDetails>();

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[GetAllFarmerLandDetails]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var farmerLandDetails = new FarmerLandDetails
                    {
                        FarmerLandDetailsId = reader["FarmerLandDetailsId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerLandDetailsId"]) : (int?)null,
                        FarmerId = reader["FarmerId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerId"]) : (int?)null,
                        LandLocation = reader["LandLocation"] != DBNull.Value ? Convert.ToString(reader["LandLocation"]) : string.Empty,
                        Zipcode = reader["Zipcode"] != DBNull.Value ? Convert.ToInt32(reader["Zipcode"]) : (int?)null,
                        LandSize = reader["LandSize"] != DBNull.Value ? Convert.ToDecimal(reader["LandSize"]) : (decimal?)null,
                        IsElectricityAvailable = reader["IsElectricityAvailable"] != DBNull.Value && Convert.ToBoolean(reader["IsElectricityAvailable"]),
                        IsWaterAvailable = reader["IsWaterAvailable"] != DBNull.Value && Convert.ToBoolean(reader["IsWaterAvailable"]),
                        SoilTypeId = reader["SoilTypeId"] != DBNull.Value ? Convert.ToInt16(reader["SoilTypeId"]) : (short?)null,
                        LastCrop = reader["LastCrop"] != DBNull.Value ? Convert.ToString(reader["LastCrop"]) : string.Empty,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : string.Empty,
                        UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"]),
                        FarmerName = reader["FarmerName"] != DBNull.Value ? Convert.ToString(reader["FarmerName"]) : string.Empty,
                        SoilTypeName = reader["SoilTypeName"] != DBNull.Value ? Convert.ToString(reader["SoilTypeName"]) : string.Empty
                    };

                    farmers.Add(farmerLandDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all farmer land details");
            }

            return farmers;
        }

        public async Task<ResultStatus> Create(CreateFarmerLandDetails createFarmerLandDetails)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                using var sqlCommand = new SqlCommand("[dbo].[CreateFarmerLandDetails]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerId", SqlDbType.Int) { Value = createFarmerLandDetails.FarmerId });
                sqlCommand.Parameters.Add(new SqlParameter("@LandLocation", SqlDbType.VarChar, 50) { Value = createFarmerLandDetails.LandLocation });
                sqlCommand.Parameters.Add(new SqlParameter("@Zipcode", SqlDbType.Int) { Value = (object)createFarmerLandDetails.Zipcode ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@LandSize", SqlDbType.Decimal) { Value = (object)createFarmerLandDetails.LandSize ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@IsElectricityAvailable", SqlDbType.Bit) { Value = createFarmerLandDetails.IsElectricityAvailable });
                sqlCommand.Parameters.Add(new SqlParameter("@IsWaterAvailable", SqlDbType.Bit) { Value = createFarmerLandDetails.IsWaterAvailable });
                sqlCommand.Parameters.Add(new SqlParameter("@SoilTypeId", SqlDbType.SmallInt) { Value = (object)createFarmerLandDetails.SoilTypeId ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@LastCrop", SqlDbType.VarChar, 50) { Value = createFarmerLandDetails.LastCrop });
                sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50) { Value = createFarmerLandDetails.CreatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating FarmerLandDetails");
                return ResultStatus.Failed;
            }
        }
    }
}
