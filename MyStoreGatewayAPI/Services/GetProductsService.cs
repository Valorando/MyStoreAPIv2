using MyStoreGatewayAPI.Interfaces;

namespace MyStoreGatewayAPI.Services
{
    public class GetProductsService : IGetProductsService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _httpQueries;

        public GetProductsService(Dictionary<string, string> httpQueries, HttpClient httpClient)
        {
            _httpQueries = httpQueries;
            _httpClient = httpClient;
        }
        public async Task<string> GetAllProductsRequest()
        {
            string apiUrl = _httpQueries["GetProductsURL"];

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
