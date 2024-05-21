using Microsoft.AspNetCore.Mvc;
using MyStoreGatewayAPI.Interfaces;
using MyStoreGatewayAPI.Models;

namespace MyStoreGatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddOrderController : ControllerBase
    {
        private readonly IAddOrderService _addOrderService;
        private readonly ILogger<AddOrderController> _logger;

        public AddOrderController(IAddOrderService addOrderService, ILogger<AddOrderController> logger)
        {
            _addOrderService = addOrderService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderModel orderModel)
        {
            try
            {
                if (orderModel == null)
                {
                    return BadRequest("Order data is null.");
                }
                _logger.LogInformation($"Client send new order: OrderId={orderModel.OrderId}, OrderDate={orderModel.OrderDate}, FirstName={orderModel.FirstName}, SecondName={orderModel.SecondName}, PhoneNumber={orderModel.PhoneNumber}, ProductName={orderModel.ProductName}, Amount={orderModel.Amount}");
                OrderModel createdOrder = await _addOrderService.AddOrderRequest(orderModel);
                return CreatedAtAction(nameof(AddOrder), new { id = createdOrder.OrderId }, createdOrder);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the order. OrderId={OrderId}, OrderDate={OrderDate}, FirstName={FirstName}, SecondName={SecondName}, PhoneNumber={PhoneNumber}, ProductName={ProductName}, Amount={Amount}",
                    orderModel.OrderId, orderModel.OrderDate, orderModel.FirstName, orderModel.SecondName, orderModel.PhoneNumber, orderModel.ProductName, orderModel.Amount);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
