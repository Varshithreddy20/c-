using CropDev.Models;
using CropDev.Repository.Interface;
using CropDev.Utilities;
using CropDev.Utilities.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using CropDev.Models.PriceQuote;

namespace CropDev.Repository.Concrete
{
    public class PriceQuoteRepository : IPriceQuoteRepository
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly ILogger<PriceQuoteRepository> logger;

        public PriceQuoteRepository(IOptions<AppSettings> appSettings, ILogger<PriceQuoteRepository> logger)
        {
            this.appSettings = appSettings;
            this.logger = logger;
        }
        public async Task<ResultStatus> Update(UpdatePriceQuote updatePriceQuote)
        {
            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[UpdatePriceQuote]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@PriceQuoteId", SqlDbType.Int) { Value = updatePriceQuote.PriceQuoteId });
                sqlCommand.Parameters.Add(new SqlParameter("@LandSize", SqlDbType.Decimal) { Value = (object)updatePriceQuote.LandSize ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@QuoteAmount", SqlDbType.Decimal) { Value = (object)updatePriceQuote.QuoteAmount ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar, 50) { Value = (object)updatePriceQuote.UpdatedBy ?? DBNull.Value });
                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int resultStatus = (int)outputParameter.Value;
                return resultStatus == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL Error updating PriceQuote: {0}", ex.Message);
                return ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating PriceQuote: {0}", ex.Message);
                return ResultStatus.Failed;
            }
        }

        public async Task<PriceQuote> GetById(int priceQuoteId)
        {
            PriceQuote? priceQuote = null;

            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[GetPriceQuoteById]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@PriceQuoteId", SqlDbType.Int) { Value = priceQuoteId });

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    priceQuote = new PriceQuote
                    {
                        PriceQuoteId = reader["PriceQuoteId"] != DBNull.Value ? Convert.ToInt32(reader["PriceQuoteId"]) : (int?)null,
                        LandSize = reader["LandSize"] != DBNull.Value ? Convert.ToDecimal(reader["LandSize"]) : (decimal?)null,
                        QuoteAmount = reader["QuoteAmount"] != DBNull.Value ? Convert.ToDecimal(reader["QuoteAmount"]) : (decimal?)null,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : string.Empty,
                        UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"])
                    };
                }
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL Error getting PriceQuote by ID: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting PriceQuote by ID: {0}", ex.Message);
            }

            return priceQuote;
        }


        public async Task<ResultStatus> Create(CreatePriceQuote createPriceQuote)
        {
            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[CreatePriceQuote]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@LandSize", SqlDbType.Decimal) { Value = (object)createPriceQuote.LandSize ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@QuoteAmount", SqlDbType.Decimal) { Value = (object)createPriceQuote.QuoteAmount ?? DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50) { Value = (object)createPriceQuote.CreatedBy ?? DBNull.Value });
                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int resultStatus = (int)outputParameter.Value;
                if (resultStatus == 1) // Assuming 1 indicates success
                {
                    return ResultStatus.Success;
                }

                return ResultStatus.Failed;
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL Error creating PriceQuote: {0}", ex.Message);
                return ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating PriceQuote: {0}", ex.Message);
                return ResultStatus.Failed;
            }
        }

        public async Task<List<PriceQuote>> GetAll()
        {
            var priceQuotes = new List<PriceQuote>();

            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[GetAllPriceQuote]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var priceQuote = new PriceQuote
                    {
                        PriceQuoteId = reader["PriceQuoteId"] != DBNull.Value ? Convert.ToInt32(reader["PriceQuoteId"]) : (int?)null,
                        LandSize = reader["LandSize"] != DBNull.Value ? Convert.ToDecimal(reader["LandSize"]) : (decimal?)null,
                        QuoteAmount = reader["QuoteAmount"] != DBNull.Value ? Convert.ToDecimal(reader["QuoteAmount"]) : (decimal?)null,
                        CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToString(reader["CreatedBy"]) : string.Empty,
                        CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                        UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToString(reader["UpdatedBy"]) : string.Empty,
                        UpdatedOn = Convert.ToDateTime(reader["UpdatedOn"])
                    };

                    priceQuotes.Add(priceQuote);
                }
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL Error getting all PriceQuotes: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all PriceQuotes: {0}", ex.Message);
            }

            return priceQuotes;
        }
        public async Task<ResultStatus> SoftDelete(int priceQuoteId, string updatedBy)
        {
            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[SoftDeletePriceQuote]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@PriceQuoteId", SqlDbType.Int) { Value = priceQuoteId });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar, 50) { Value = updatedBy });
                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int resultStatus = (int)outputParameter.Value;
                return resultStatus == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL Error soft deleting PriceQuote: {0}", ex.Message);
                return ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error soft deleting PriceQuote: {0}", ex.Message);
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Restore(int priceQuoteId, string updatedBy)
        {
            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[RestorePriceQuote]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@PriceQuoteId", SqlDbType.Int) { Value = priceQuoteId });
                sqlCommand.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.NVarChar, 50) { Value = updatedBy });
                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int resultStatus = (int)outputParameter.Value;
                return resultStatus == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL Error restoring PriceQuote: {0}", ex.Message);
                return ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error restoring PriceQuote: {0}", ex.Message);
                return ResultStatus.Failed;
            }
        }

    }
}
