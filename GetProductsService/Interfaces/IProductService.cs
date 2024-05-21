using GetProductsService.Models;

namespace GetProductsService.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductModel>> GetAllProducts();
    }
}
