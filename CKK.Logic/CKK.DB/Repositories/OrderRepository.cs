using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;

namespace CKK.DB.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public OrderRepository(IConnectionFactory conn)
        {
            _connectionFactory = conn;
        }

        public int Add(Order entity) // Add a order to the database
        {
            var sql = "INSERT INTO Orders (OrderId,OrderNumber,CustomerId,ShoppingCartId) VALUES (@OrderId,@OrderNumber,@CustomerId,@ShoppingCartId)";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public int Delete(int id) // Delete a order from the database
        {
            var sql = "DELETE FROM Orders WHERE OrderId = @OrderId";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, new { OrderId = id });
                return result;
            }
        }

        public List<Order> GetAll() // Get all orders from the database
        {
            var sql = "SELECT * FROM Orders";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<Order>(sql).ToList();
                return result;
            }
        }

        public Order GetById(int id) // Get an order by ID
        {
            var sql = "SELECT * FROM Orders WHERE OrderId = @OrderId";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Order>(sql, new { OrderId = id });
                return result;
            }
        }

        public Order GetOrderByCustomerId(int id) // Get an order by Customer ID
        {
            var sql = "SELECT * FROM Orders WHERE CustomerId = @CustomerId";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Order>(sql, new { CustomerId = id });
                return result;
            }
        }

        public int Update(Order entity) // Update order
        {
            var sql = "UPDATE Orders SET OrderNumber = @OrderNumber, CustomerId = @CustomerId, ShoppingCartId = @ShoppingCartId WHERE OrderId = @OrderId";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }
    }
}
