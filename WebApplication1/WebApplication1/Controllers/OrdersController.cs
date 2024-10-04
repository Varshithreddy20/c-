using CRAVENEST.Model;
using CRAVENEST.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        _logger.LogInformation("CreateOrder action called with Order: {@Order}", order);

        try
        {
            var orderId = await _orderService.CreateOrder(order);
            _logger.LogInformation("Order created successfully with OrderId: {OrderId}", orderId);
            return Ok(new { OrderId = orderId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating order.");
            return StatusCode(500, "An error occurred while creating the order.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        _logger.LogInformation("GetAllOrders action called.");

        try
        {
            var orders = await _orderService.GetAllOrders();
            _logger.LogInformation("Retrieved {OrderCount} orders.");
            return Ok(orders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving orders.");
            return StatusCode(500, "An error occurred while retrieving the orders.");
        }
    }

    [HttpPatch("{id}/{status}")]
    public async Task<IActionResult> UpdateOrderStatus(int id,string status)
    {
        _logger.LogInformation("UpdateOrderStatus action called for OrderId: {OrderId} with Status: {Status}", id, status);

        try
        {
            var updated = await _orderService.UpdateOrderStatus(id, status);
            if (updated != 0)
            {
                _logger.LogInformation("Order status updated successfully for OrderId: {OrderId}", id);
                return Ok();
            }
            else
            {
                _logger.LogWarning("Failed to update order status for OrderId: {OrderId}", id);
                return StatusCode(500, "Error updating order status.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating order status for OrderId: {OrderId}", id);
            return StatusCode(500, "An error occurred while updating the order status.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        _logger.LogInformation("GetOrderById action called for OrderId: {OrderId}", id);

        try
        {
            var order = await _orderService.GetOrderById(id);
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                _logger.LogWarning("Order not found for OrderId: {OrderId}", id);
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving order details for OrderId: {OrderId}", id);
            return StatusCode(500, "An error occurred while retrieving the order details.");
        }
    }

    [HttpGet("signup/{signUpId}")]
    public async Task<IActionResult> GetOrdersBySignupId(int signUpId)
    {
        try
        {
            var orders = await _orderService.GetOrdersBySignupId(signUpId);
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orders for SignUpId: {SignUpId}", signUpId);
            return StatusCode(500, "An error occurred while retrieving the orders.");
        }
    }


}
