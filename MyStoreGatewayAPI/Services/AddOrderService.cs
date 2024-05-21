using MyStoreGatewayAPI.Interfaces;
using MyStoreGatewayAPI.Models;

namespace MyStoreGatewayAPI.Services
{
    public class AddOrderService : IAddOrderService
    {
        private readonly Dictionary<string, string> _httpRequests;
        private readonly HttpClient _httpClient;

        public AddOrderService(Dictionary<string, string> httpRequests, HttpClient httpClient)
        {
            _httpRequests = httpRequests;
            _httpClient = httpClient;
        }

        public async Task<OrderModel> AddOrderRequest(OrderModel orderModel)
        {
            string apiUrl = _httpRequests["AddOrderURL"];

            var content = JsonContent.Create(orderModel);
            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
            return orderModel;
        }
    }
}
