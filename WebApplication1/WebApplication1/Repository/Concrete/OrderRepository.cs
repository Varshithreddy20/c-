using CRAVENEST.Model;
using CRAVENEST.Repository.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(IConfiguration configuration, ILogger<OrderRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _logger = logger;
    }

    public async Task<int> CreateOrder(Order order)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("[dbo].[CreateOrder]", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CustomerName", order.CustomerName);
            command.Parameters.AddWithValue("@CustomerEmail", order.CustomerEmail);
            command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
            command.Parameters.AddWithValue("@SignUpId", order.SignUpId);

            // Convert the list of items to a JSON string
            var orderItemsJson = JsonConvert.SerializeObject(order.Items.Select(item => new {
                item.FoodItemId,
                item.Quantity,
                item.UnitPrice
            }));
            command.Parameters.AddWithValue("@OrderItems", orderItemsJson);

            var orderIdParam = new SqlParameter("@OrderId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            command.Parameters.Add(orderIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            int orderId = (int)orderIdParam.Value;
            _logger.LogInformation("Order created successfully. OrderId: {OrderId}", orderId);

            return orderId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order for Customer: {CustomerName}, Email: {CustomerEmail}", order.CustomerName, order.CustomerEmail);
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        try
        {
            var orders = new List<Order>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("[dbo].[GetAllOrders]", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var order = new Order
                {
                    OrderId = (int)reader["OrderId"],
                    CustomerName = reader["CustomerName"].ToString(),
                    CustomerEmail = reader["CustomerEmail"].ToString(),
                    TotalAmount = (decimal)reader["TotalAmount"],
                    Status = reader["Status"].ToString(),
                    CreatedAt = (DateTime)reader["CreatedAt"],
                    UpdatedAt = reader["UpdatedAt"] as DateTime?
                };
                orders.Add(order);
            }

            _logger.LogInformation("Successfully retrieved {OrderCount} orders.", orders.Count);
            return orders;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all orders.");
            throw;
        }
    }

    public async Task<int> UpdateOrderStatus(int orderId, string status)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("[dbo].[UpdateOrderStatus]", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@OrderId", orderId);
            command.Parameters.AddWithValue("@Status", status);

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            _logger.LogInformation("Updated order status for OrderId: {OrderId} to {Status}. Rows affected: {RowsAffected}", orderId, status, rowsAffected);
            return rowsAffected;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating status for OrderId: {OrderId} to {Status}.", orderId, status);
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersBySignupId(int signUpId)
    {
        try
        {
            var orders = new Dictionary<int, Order>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("[dbo].[GetOrdersBySignupId]", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Add parameter for the stored procedure
            command.Parameters.AddWithValue("@SignUpId", signUpId);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int orderId = (int)reader["OrderId"];

                // Check if the order already exists in the dictionary
                if (!orders.ContainsKey(orderId))
                {
                    // Create a new order object and add it to the dictionary
                    var order = new Order
                    {
                        OrderId = orderId,
                        CustomerName = reader["CustomerName"].ToString(),
                        CustomerEmail = reader["CustomerEmail"].ToString(),
                        TotalAmount = (decimal)reader["TotalAmount"],
                        Status = reader["Status"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"],
                        UpdatedAt = reader["UpdatedAt"] as DateTime?,
                        SignUpId = signUpId,
                        Items = new List<OrderItem>()
                    };

                    orders[orderId] = order;
                }

                // Check if there are any order items in the result
                if (!reader.IsDBNull(reader.GetOrdinal("OrderItemId")))
                {
                    // Create an order item object and add it to the order's Items list
                    var orderItem = new OrderItem
                    {
                        OrderItemId = reader.GetInt32(reader.GetOrdinal("OrderItemId")),
                        FoodItemId = reader.GetInt32(reader.GetOrdinal("FoodItemId")),
                        FoodItemName = reader["FoodItemName"].ToString(),
                        Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                        UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice"))
                    };

                    orders[orderId].Items.Add(orderItem);
                }
            }

            _logger.LogInformation("Successfully retrieved orders for SignUpId: {SignUpId}", signUpId);

            // Return the orders as a list
            return orders.Values.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orders for SignUpId: {SignUpId}", signUpId);
            throw;
        }
    }



    public async Task<Order> GetOrderById(int orderId)
    {
        try
        {
            Order order = null;
            var orderItems = new List<OrderItem>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("[dbo].[GetOrderDetails]", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@OrderId", orderId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                if (order == null)
                {
                    // Initialize the order object once with basic details
                    order = new Order
                    {
                        OrderId = (int)reader["OrderId"],
                        CustomerName = reader["CustomerName"].ToString(),
                        CustomerEmail = reader["CustomerEmail"].ToString(),
                        TotalAmount = (decimal)reader["TotalAmount"],
                        Status = reader["Status"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"],
                        Items = new List<OrderItem>()
                    };
                }

                // Add each order item to the list
                var orderItem = new OrderItem
                {
                    OrderItemId = (int)reader["OrderItemId"],
                    FoodItemId = (int)reader["FoodItemId"],
                    FoodItemName = reader["FoodItemName"].ToString(),
                    Quantity = (int)reader["Quantity"],
                    UnitPrice = (decimal)reader["UnitPrice"]
                };
                order.Items.Add(orderItem);
            }

            _logger.LogInformation("Successfully retrieved order details for OrderId: {OrderId}", orderId);
            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order details for OrderId: {OrderId}", orderId);
            throw;
        }
    }

}
