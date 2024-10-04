using CRAVENEST.Model;
using CRAVENEST.Repository.Interface;
using CRAVENEST.Service.Interface;
using Microsoft.Extensions.Logging;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<int> CreateOrder(Order order)
    {
        return await _orderRepository.CreateOrder(order);
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await _orderRepository.GetAllOrders();
    }

    public async Task<int> UpdateOrderStatus(int orderId, string status)
    {
        return await _orderRepository.UpdateOrderStatus(orderId, status);
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        return await _orderRepository.GetOrderById(orderId);
    }
    public async Task<IEnumerable<Order>> GetOrdersBySignupId(int signUpId)
    {
        return await _orderRepository.GetOrdersBySignupId(signUpId);
    }


}
