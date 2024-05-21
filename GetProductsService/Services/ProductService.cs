using MySql.Data.MySqlClient;
using System.Data;
using GetProductsService.Models;
using GetProductsService.Interfaces;

namespace GetProductsService.Services
{
    public class ProductService : IProductService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public ProductService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            var products = new List<ProductModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = _sqlQueries["GetAllProducts"];

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var product = new ProductModel
                        {
                            ProductId = reader.GetInt32("ProductId"),
                            Name = reader.GetString("Name"),
                            Amount = reader.GetInt32("Amount"),
                            Price = reader.GetString("Price")
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }
    }
}

