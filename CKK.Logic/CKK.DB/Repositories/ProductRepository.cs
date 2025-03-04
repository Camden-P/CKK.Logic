using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;

namespace CKK.DB.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ProductRepository(IConnectionFactory conn)
        {
            _connectionFactory = conn;
        }

        public int Add(Product entity) // Add a product to the database
        {
            var sql = "INSERT INTO Products (Price,Quantity,Name) VALUES (@Price,@Quantity,@Name)";
            
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public int Delete(int id) // Delete a product from the database
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, new { Id = id });
                return result;
            }
        }

        public List<Product> GetAll() // Get all products from the database
        {
            var sql = "SELECT * FROM Products";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<Product>(sql).ToList();
                return result;
            }
        }

        public List<Product> GetAllByID() // Get all products by ID
        {
            var sql = "SELECT * FROM Products ORDER BY Id";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<Product>(sql).ToList();

                // Sort by ID
                for (int i = 0; i < result.Count - 1; ++i)
                {
                    for (int j = i + 1; j < result.Count; ++j)
                    {
                        if (result[j].Id < result[i].Id)
                        {
                            SwapItems(ref result, i, j);
                        }
                    }
                }

                return result;
            }
        }

        public List<Product> GetAllByQuantity() // Get all products by quantity
        {
            var sql = "SELECT * FROM Products";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<Product>(sql).ToList();

                // Sort by Quantity
                for (int i = 0; i < result.Count - 1; ++i)
                {
                    for (int j = i + 1; j < result.Count; ++j)
                    {
                        if (result[j].Quantity > result[i].Quantity)
                        {
                            SwapItems(ref result, i, j);
                        }
                    }
                }

                return result;
            }
        }

        public List<Product> GetAllByPrice() // Get all products by price
        {
            var sql = "SELECT * FROM Products ORDER BY Price";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<Product>(sql).ToList();

                // Sort by Price
                for (int i = 0; i < result.Count - 1; ++i)
                {
                    for (int j = i + 1; j < result.Count; ++j)
                    {
                        if (result[j].Price > result[i].Price)
                        {
                            SwapItems(ref result, i, j);
                        }
                    }
                }

                return result;
            }
        }

        public Product GetById(int id) // Get a single product by ID
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
                return result;
            }
        }

        public List<Product> GetByName(string name) // Search for a product by it's Name
        {
            var sql = "SELECT * FROM Products WHERE Name LIKE CONCAT('%',@Name,'%')";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<Product>(sql, new { Name = name }).ToList();
                return result;
            }
        }

        public int Update(Product entity) // Update product
        {
            var sql = "UPDATE Products SET Price = @Price, Quantity = @Quantity, Name = @Name WHERE Id = @Id";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        private void SwapItems(ref List<Product> items, int i, int j) // Swap two products for sorting
        {
            var temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
    }
}
