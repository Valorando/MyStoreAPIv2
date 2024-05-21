namespace MyStoreGatewayAPI.Interfaces
{
    public interface IGetProductsService
    {
        public Task<string> GetAllProductsRequest();
    }
}
