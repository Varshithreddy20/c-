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

namespace CropDev.Repository.Concrete
{
    public class FarmerLandDetailsRepository : IFarmerLandDetailsRepository
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly ILogger<FarmerLandDetailsRepository> logger;

        public FarmerLandDetailsRepository(IOptions<AppSettings> appSettings, ILogger<FarmerLandDetailsRepository> logger)
        {
            this.appSettings = appSettings;
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmerLandDetailsId"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
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
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand(storedProcedure, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

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
                logger.LogError(ex, $"Error executing {storedProcedure} for FarmerId {farmerLandDetailsId}");
                return ResultStatus.Failed;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmerLandDetailsId"></param>
        /// <returns></returns>
        public async Task<FarmerLandDetails> GetById(int farmerLandDetailsId)
        {
            FarmerLandDetails? farmerLandDetails = null;

            try
            {
                using (var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (var sqlCommand = new SqlCommand("[dbo].[GetFarmerLandDetailById]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@FarmerLandDetailsId", SqlDbType.Int) { Value = farmerLandDetailsId });

                        using (var reader = await sqlCommand.ExecuteReaderAsync())
                        {
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
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting farmer land details by ID");
            }

            return farmerLandDetails;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UpdateFarmerLandDetails"></param>
        /// <returns></returns>
        public async Task<ResultStatus> Update(UpdateFarmerLandDetails UpdateFarmerLandDetails)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (var sqlCommand = new SqlCommand("[dbo].[UpdateFarmerLandDetails]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        // Input parameters
                        sqlCommand.Parameters.Add(new SqlParameter("@FarmerLandDetailsId", SqlDbType.Int) { Value = UpdateFarmerLandDetails.FarmerLandDetailsId });
                        sqlCommand.Parameters.Add(new SqlParameter("@FarmerId", SqlDbType.Int) { Value = UpdateFarmerLandDetails.FarmerId });
                        sqlCommand.Parameters.Add(new SqlParameter("@LandLocation", SqlDbType.VarChar, 50) { Value = UpdateFarmerLandDetails.LandLocation });
                        sqlCommand.Parameters.Add(new SqlParameter("@Zipcode", SqlDbType.Int) { Value = (object)UpdateFarmerLandDetails.Zipcode ?? DBNull.Value });
                        sqlCommand.Parameters.Add(new SqlParameter("@LandSize", SqlDbType.Decimal) { Value = (object)UpdateFarmerLandDetails.LandSize ?? DBNull.Value });
                        sqlCommand.Parameters.Add(new SqlParameter("@IsElectricityAvailable", SqlDbType.Bit) { Value = UpdateFarmerLandDetails.IsElectricityAvailable });
                        sqlCommand.Parameters.Add(new SqlParameter("@IsWaterAvailable", SqlDbType.Bit) { Value = UpdateFarmerLandDetails.IsWaterAvailable });
                        sqlCommand.Parameters.Add(new SqlParameter("@SoilTypeId", SqlDbType.SmallInt) { Value = (object)UpdateFarmerLandDetails.SoilTypeId ?? DBNull.Value });
                        sqlCommand.Parameters.Add(new SqlParameter("@LastCrop", SqlDbType.VarChar, 50) { Value = UpdateFarmerLandDetails.LastCrop });
                        sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar, 50) { Value = UpdateFarmerLandDetails.UpdatedBy });

                        // Output parameter
                        var statusParameter = new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        sqlCommand.Parameters.Add(statusParameter);

                        // Execute the stored procedure
                        await sqlCommand.ExecuteNonQueryAsync();

                        // Check the status output
                        int status = Convert.ToInt32(sqlCommand.Parameters["@StatusOutPut"].Value);

                        return status == 1 ? ResultStatus.Success : ResultStatus.Failed;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating FarmerLandDetails");
                return ResultStatus.Failed;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<FarmerLandDetails>> GetAll()
        {
            List<FarmerLandDetails> farmers = new List<FarmerLandDetails>();

            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[GetAllFarmerLandDetails]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                await sqlConnection.OpenAsync();

                using var reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    FarmerLandDetails farmerLandDetails = new FarmerLandDetails
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
                logger.LogError(ex, "Error getting all farmer land details");
            }

            return farmers;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createFarmerLandDetails"></param>
        /// <returns></returns>
        public async Task<ResultStatus> Create(CreateFarmerLandDetails createFarmerLandDetails)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (var sqlCommand = new SqlCommand("[dbo].[CreateFarmerLandDetails]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        // Input parameters
                        sqlCommand.Parameters.Add(new SqlParameter("@FarmerId", SqlDbType.Int) { Value = createFarmerLandDetails.FarmerId });
                        sqlCommand.Parameters.Add(new SqlParameter("@LandLocation", SqlDbType.VarChar, 50) { Value = createFarmerLandDetails.LandLocation });
                        sqlCommand.Parameters.Add(new SqlParameter("@Zipcode", SqlDbType.Int) { Value = (object)createFarmerLandDetails.Zipcode ?? DBNull.Value });
                        sqlCommand.Parameters.Add(new SqlParameter("@LandSize", SqlDbType.Decimal) { Value = (object)createFarmerLandDetails.LandSize ?? DBNull.Value });
                        sqlCommand.Parameters.Add(new SqlParameter("@IsElectricityAvailable", SqlDbType.Bit) { Value = createFarmerLandDetails.IsElectricityAvailable });
                        sqlCommand.Parameters.Add(new SqlParameter("@IsWaterAvailable", SqlDbType.Bit) { Value = createFarmerLandDetails.IsWaterAvailable });
                        sqlCommand.Parameters.Add(new SqlParameter("@SoilTypeId", SqlDbType.SmallInt) { Value = (object)createFarmerLandDetails.SoilTypeId ?? DBNull.Value });
                        sqlCommand.Parameters.Add(new SqlParameter("@LastCrop", SqlDbType.VarChar, 50) { Value = createFarmerLandDetails.LastCrop });
                        sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50) { Value = createFarmerLandDetails.CreatedBy });

                        // Output parameter
                        var statusParameter = new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        sqlCommand.Parameters.Add(statusParameter);

                        // Execute the stored procedure
                        await sqlCommand.ExecuteNonQueryAsync();

                        // Check the status output
                        int status = Convert.ToInt32(sqlCommand.Parameters["@StatusOutPut"].Value);

                        return status == 1 ? ResultStatus.Success : ResultStatus.Failed;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating FarmerLandDetails");
                return ResultStatus.Failed;
            }
        }
    }
}