using Microsoft.AspNetCore.Mvc;
using AddOrderService.Interfaces;
using AddOrderService.Models;

namespace AddOrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> Post([FromBody] OrderModel orderModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _logger.LogInformation($"Client send new order: OrderId={orderModel.OrderId}, OrderDate={orderModel.OrderDate}, FirstName={orderModel.FirstName}, SecondName={orderModel.SecondName}, PhoneNumber={orderModel.PhoneNumber}, ProductName={orderModel.ProductName}, Amount={orderModel.Amount}");

                await _orderService.AddOrder(orderModel);

                return CreatedAtAction(nameof(Post), null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the order. OrderId={OrderId}, OrderDate={OrderDate}, FirstName={FirstName}, SecondName={SecondName}, PhoneNumber={PhoneNumber}, ProductName={ProductName}, Amount={Amount}",
                    orderModel.OrderId, orderModel.OrderDate, orderModel.FirstName, orderModel.SecondName, orderModel.PhoneNumber, orderModel.ProductName, orderModel.Amount);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
