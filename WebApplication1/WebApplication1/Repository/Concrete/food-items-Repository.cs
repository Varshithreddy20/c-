using CRAVENEST.Model;
using CRAVENEST.Repository.Interface;
using CRAVENEST.Utilities;
using CRAVENEST.Utilities.Eums;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace CRAVENEST.Repository.Concrete
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FoodItemRepository> _logger;

        public FoodItemRepository(IOptions<AppSettings> appSettings, ILogger<FoodItemRepository> logger, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<ResultStatus> Create(FoodItem foodItem)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[AddFoodItem]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = foodItem.Name });
                sqlCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 255) { Value = foodItem.Description });
                sqlCommand.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Value = foodItem.Price });
                sqlCommand.Parameters.Add(new SqlParameter("@ImagePath", SqlDbType.NVarChar, 255) { Value = foodItem.Image });
                sqlCommand.Parameters.Add(new SqlParameter("@Cuisine", SqlDbType.NVarChar, 50) { Value = foodItem.Cuisine });
                sqlCommand.Parameters.Add(new SqlParameter("@Available", SqlDbType.Bit) { Value = foodItem.Available });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                return ResultStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating food item");
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Delete(int id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[DeleteFoodItem]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                return ResultStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting food item with id {id}");
                return ResultStatus.Failed;
            }
        }

        public async Task<List<FoodItem>> GetAll()
        {
            var foodItems = new List<FoodItem>();

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[GetAllFoodItems]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var foodItem = new FoodItem
                    {
                        FoodItemId = Convert.ToInt32(reader["FoodItemId"]),
                        Name = Convert.ToString(reader["Name"]),
                        Description = Convert.ToString(reader["Description"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Image = Convert.ToString(reader["ImagePath"]),
                        Cuisine = Convert.ToString(reader["Cuisine"]),
                        Available = reader["Available"] != DBNull.Value && Convert.ToBoolean(reader["Available"]),
                        CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : null,
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : null
                    };

                    foodItems.Add(foodItem);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all food items");
            }

            return foodItems;
        }

        public async Task<List<FoodItem>> GetByCuisine(string cuisine)
        {
            var foodItems = new List<FoodItem>();

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[GetFoodItemsByCuisine]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@Cuisine", SqlDbType.NVarChar, 50) { Value = cuisine });

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var foodItem = new FoodItem
                    {
                        FoodItemId = Convert.ToInt32(reader["FoodItemId"]),
                        Name = Convert.ToString(reader["Name"]),
                        Description = Convert.ToString(reader["Description"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Image = Convert.ToString(reader["ImagePath"]),
                        Cuisine = Convert.ToString(reader["Cuisine"]),
                        Available = reader["Available"] != DBNull.Value && Convert.ToBoolean(reader["Available"]),
                        CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : null,
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : null
                    };

                    foodItems.Add(foodItem);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting food items for cuisine {cuisine}");
            }

            return foodItems;
        }

        public async Task<ResultStatus> UpdateAvailability(int id, bool available)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[UpdateFoodItemAvailability]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@FoodItemId", SqlDbType.Int) { Value = id });
                sqlCommand.Parameters.Add(new SqlParameter("@Available", SqlDbType.Bit) { Value = available });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                return ResultStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating availability for food item with id {id}");
                return ResultStatus.Failed;
            }
        }
    }
}