using CropDev.Models;
using CropDev.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CropDev.Utilities;
using CropDev.Utilities.Enums;
using Microsoft.Extensions.Logging;

namespace CropDev.Repository.Concrete
{
    public class FarmersRepository : IFarmersRepository
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly ILogger<FarmersRepository> logger;

        public FarmersRepository(IOptions<AppSettings> appSettings, ILogger<FarmersRepository> logger)
        {
            this.appSettings = appSettings;
            this.logger = logger;
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
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand(storedProcedure, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

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
                logger.LogError(ex, $"Error executing {storedProcedure} for FarmerId {farmerId}");
                return ResultStatus.Failed;
            }
        }

        public async Task<Farmers> GetById(int farmerId)
        {
            Farmers farmer = null;

            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[GetFarmerById]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerId", SqlDbType.Int) { Value = farmerId });

                await sqlConnection.OpenAsync();

                using (var reader = await sqlCommand.ExecuteReaderAsync())
                {
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
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error getting farmer by id {farmerId}");
                return farmer;
            }

            return farmer;
        }

        public async Task<ResultStatus> Update(Farmers farmers)
        {
            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[UpdateFarmer]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerId", SqlDbType.Int) { Value = farmers.FarmerId });
                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar) { Value = farmers.FirstName });
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar) { Value = farmers.LastName });
                sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar) { Value = farmers.City });
                sqlCommand.Parameters.Add(new SqlParameter("@State", SqlDbType.VarChar) { Value = farmers.State });
                sqlCommand.Parameters.Add(new SqlParameter("@ZipCode", SqlDbType.Int) { Value = farmers.ZipCode });
                sqlCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar) { Value = farmers.Country });
                sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar) { Value = farmers.PhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@SecondaryPhoneNumber", SqlDbType.VarChar) { Value = farmers.SecondaryPhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar) { Value = farmers.UpdatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error updating farmer with id {farmers.FarmerId}");
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Create(Farmers farmers)
        {
            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[CreateFarmer]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = farmers.FirstName });
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = farmers.LastName });
                sqlCommand.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 100) { Value = farmers.City });
                sqlCommand.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar, 50) { Value = farmers.State });
                sqlCommand.Parameters.Add(new SqlParameter("@ZipCode", SqlDbType.Int) { Value = farmers.ZipCode });
                sqlCommand.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar, 50) { Value = farmers.Country });
                sqlCommand.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 20) { Value = farmers.PhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@SecondaryPhoneNumber", SqlDbType.NVarChar, 20) { Value = farmers.SecondaryPhoneNumber });
                sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50) { Value = farmers.CreatedBy });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar, 50) { Value = farmers.UpdatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error creating farmer");
                return ResultStatus.Failed;
            }
        }

        public async Task<List<Farmers>> GetAll()
        {
            List<Farmers> farmers = new List<Farmers>();

            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[GetAllFarmers]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                await sqlConnection.OpenAsync();

                using var reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Farmers farmer = new Farmers
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
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : null,
                        UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"])
                    };

                    farmers.Add(farmer);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all farmers");
                return farmers;
            }

            return farmers;
        }
    }
}
