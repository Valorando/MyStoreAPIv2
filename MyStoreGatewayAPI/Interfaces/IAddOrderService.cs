using MyStoreGatewayAPI.Models;

namespace MyStoreGatewayAPI.Interfaces
{
    public interface IAddOrderService
    {
        public Task<OrderModel> AddOrderRequest(OrderModel orderModel);
    }
}
