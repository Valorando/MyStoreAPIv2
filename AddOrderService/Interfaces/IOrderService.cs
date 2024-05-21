using AddOrderService.Models;

namespace AddOrderService.Interfaces
{
    public interface IOrderService
    {
        public Task AddOrder(OrderModel orderModel);
    }
}
