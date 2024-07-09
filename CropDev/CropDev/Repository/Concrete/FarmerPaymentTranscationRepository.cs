using CropDev.Models.FPT;
using CropDev.Repository.Interface;
using CropDev.Utilities.Enums;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;

namespace CropDev.Repository.Concrete
{
    public class FarmerPaymentTransactionRepository : IFarmerPaymentTransactionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FarmerPaymentTransactionRepository> _logger;

        public FarmerPaymentTransactionRepository(IConfiguration configuration, ILogger<FarmerPaymentTransactionRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ResultStatus> SoftDelete(int id, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[SoftDeleteFTPById]", id, updatedBy);
        }

        public async Task<ResultStatus> Restore(int id, string updatedBy)
        {
            return await ExecuteNonQuery("[dbo].[RestoreFTPById]", id, updatedBy);
        }

        private async Task<ResultStatus> ExecuteNonQuery(string storedProcedure, int id, string updatedBy)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await using var sqlCommand = new SqlCommand(storedProcedure, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerPaymentTransactionId", SqlDbType.Int) { Value = id });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar) { Value = updatedBy });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync().ConfigureAwait(false);
                await sqlCommand.ExecuteNonQueryAsync().ConfigureAwait(false);

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error executing {storedProcedure} for FTP Id {id}");
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Update(UpdateFarmerPaymentTransaction updateFarmerPaymentTransaction)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                await using var sqlCommand = new SqlCommand("[dbo].[UpdateFarmerPaymentTransaction]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerPaymentTransactionId", SqlDbType.Int) { Value = updateFarmerPaymentTransaction.FarmerPaymentTransactionId });
                sqlCommand.Parameters.Add(new SqlParameter("@FarmerLandDetailsId", SqlDbType.Int) { Value = updateFarmerPaymentTransaction.FarmerLandDetailsId });
                sqlCommand.Parameters.Add(new SqlParameter("@PriceQuoteId", SqlDbType.Int) { Value = updateFarmerPaymentTransaction.PriceQuoteId });
                sqlCommand.Parameters.Add(new SqlParameter("@ChargeAmount", SqlDbType.Decimal) { Value = updateFarmerPaymentTransaction.ChargeAmount });
                sqlCommand.Parameters.Add(new SqlParameter("@Discounts", SqlDbType.VarChar, 100) { Value = updateFarmerPaymentTransaction.Discounts });
                sqlCommand.Parameters.Add(new SqlParameter("@Paid", SqlDbType.Decimal) { Value = updateFarmerPaymentTransaction.Paid });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.VarChar, 100) { Value = updateFarmerPaymentTransaction.UpdatedBy });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedOn", SqlDbType.DateTime) { Value = updateFarmerPaymentTransaction.UpdatedOn });

                await sqlCommand.ExecuteNonQueryAsync();

                return ResultStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating farmer payment transaction");
                return ResultStatus.Failed;
            }
        }

        public async Task<FarmerPaymentTransaction> GetById(int farmerPaymentTransactionId)
        {
            FarmerPaymentTransaction? farmerPaymentTransaction = null;

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await using var sqlCommand = new SqlCommand("[dbo].[GetFarmerPaymentTransactionById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerPaymentTransactionId", SqlDbType.Int) { Value = farmerPaymentTransactionId });

                await sqlConnection.OpenAsync();

                await using var reader = await sqlCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    farmerPaymentTransaction = new FarmerPaymentTransaction
                    {
                        FarmerPaymentTransactionId = reader["FarmerPaymentTransactionId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerPaymentTransactionId"]) : (int?)null,
                        FarmerLandDetailsId = reader["FarmerLandDetailsId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerLandDetailsId"]) : (int?)null,
                        PriceQuoteId = reader["PriceQuoteId"] != DBNull.Value ? Convert.ToInt32(reader["PriceQuoteId"]) : (int?)null,
                        ChargeAmount = reader["ChargeAmount"] != DBNull.Value ? Convert.ToDecimal(reader["ChargeAmount"]) : (decimal?)null,
                        Discounts = reader["Discounts"] != DBNull.Value ? Convert.ToString(reader["Discounts"]) : null,
                        Paid = reader["Paid"] != DBNull.Value ? Convert.ToDecimal(reader["Paid"]) : 0,
                        Balance = reader["Balance"] != DBNull.Value ? Convert.ToDecimal(reader["Balance"]) : 0,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : null,
                        CreatedOn = reader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue,
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : null,
                        UpdatedOn = reader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedOn"]) : DateTime.MinValue
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving farmer payment transaction by id: {farmerPaymentTransactionId}");
            }

            return farmerPaymentTransaction;
        }

        public async Task<ResultStatus> Create(CreateFarmerPaymentTransaction createFarmerPaymentTransaction)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                await using var sqlCommand = new SqlCommand("[dbo].[CreateFarmerPaymentTransaction]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FarmerLandDetailsId", SqlDbType.Int) { Value = createFarmerPaymentTransaction.FarmerLandDetailsId });
                sqlCommand.Parameters.Add(new SqlParameter("@PriceQuoteId", SqlDbType.Int) { Value = createFarmerPaymentTransaction.PriceQuoteId });
                sqlCommand.Parameters.Add(new SqlParameter("@ChargeAmount", SqlDbType.Decimal) { Value = createFarmerPaymentTransaction.ChargeAmount });
                sqlCommand.Parameters.Add(new SqlParameter("@Discounts", SqlDbType.VarChar, 100) { Value = createFarmerPaymentTransaction.Discounts });
                sqlCommand.Parameters.Add(new SqlParameter("@Paid", SqlDbType.Decimal) { Value = createFarmerPaymentTransaction.Paid });
                sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.VarChar, 100) { Value = createFarmerPaymentTransaction.CreatedBy });
                sqlCommand.Parameters.Add(new SqlParameter("@CreatedOn", SqlDbType.DateTime) { Value = createFarmerPaymentTransaction.CreatedOn });

                await sqlCommand.ExecuteNonQueryAsync();

                return ResultStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating farmer payment transaction");
                return ResultStatus.Failed;
            }
        }

        public async Task<List<FarmerPaymentTransaction>> GetAll()
        {
            var farmerPaymentTransactions = new List<FarmerPaymentTransaction>();

            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                await using var sqlConnection = new SqlConnection(connectionString);
                await using var sqlCommand = new SqlCommand("[dbo].[GetAllFarmerPaymentTransactions]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await sqlConnection.OpenAsync();
                await using var reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var farmerPaymentTransaction = new FarmerPaymentTransaction
                    {
                        FarmerPaymentTransactionId = reader["FarmerPaymentTransactionId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerPaymentTransactionId"]) : (int?)null,
                        FarmerLandDetailsId = reader["FarmerLandDetailsId"] != DBNull.Value ? Convert.ToInt32(reader["FarmerLandDetailsId"]) : (int?)null,
                        PriceQuoteId = reader["PriceQuoteId"] != DBNull.Value ? Convert.ToInt32(reader["PriceQuoteId"]) : (int?)null,
                        ChargeAmount = reader["ChargeAmount"] != DBNull.Value ? Convert.ToDecimal(reader["ChargeAmount"]) : (decimal?)null,
                        Discounts = reader["Discounts"] != DBNull.Value ? Convert.ToString(reader["Discounts"]) : null,
                        Paid = reader["Paid"] != DBNull.Value ? Convert.ToDecimal(reader["Paid"]) : 0,
                        Balance = reader["Balance"] != DBNull.Value ? Convert.ToDecimal(reader["Balance"]) : 0,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : null,
                        CreatedOn = reader["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedOn"]) : DateTime.MinValue,
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : null,
                        UpdatedOn = reader["UpdatedOn"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedOn"]) : DateTime.MinValue
                    };

                    farmerPaymentTransactions.Add(farmerPaymentTransaction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all farmer payment transactions");
            }

            return farmerPaymentTransactions;
        }
    }
}
