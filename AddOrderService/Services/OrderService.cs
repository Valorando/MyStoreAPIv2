using AddOrderService.Models;
using AddOrderService.Interfaces;
using MySql.Data.MySqlClient;

namespace AddOrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public OrderService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public async Task AddOrder(OrderModel orderModel)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = _sqlQueries["AddOrder"];

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderid", orderModel.OrderId);
                    command.Parameters.AddWithValue("@orderdate", orderModel.OrderDate);
                    command.Parameters.AddWithValue("@firstname", orderModel.FirstName);
                    command.Parameters.AddWithValue("@secondname", orderModel.SecondName);
                    command.Parameters.AddWithValue("@phonenumber", orderModel.PhoneNumber);
                    command.Parameters.AddWithValue("@productname", orderModel.ProductName);
                    command.Parameters.AddWithValue("@amount", orderModel.Amount);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
