using CRAVENEST.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRAVENEST.Service.Interface
{
    public interface IOrderService
    {
        // Create an order and return the Order ID
        Task<int> CreateOrder(Order order);

        // Get all orders
        Task<IEnumerable<Order>> GetAllOrders();

        // Get a single order by ID
        //Task<Order> GetOrderById(int orderId);

        // Update the status of an order
        Task<int> UpdateOrderStatus(int orderId, string status);
        Task<Order> GetOrderById(int orderId);
        Task<IEnumerable<Order>> GetOrdersBySignupId(int signUpId);

    }
}
