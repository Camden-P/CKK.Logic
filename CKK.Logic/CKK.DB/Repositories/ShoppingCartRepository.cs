using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;

namespace CKK.DB.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ShoppingCartRepository(IConnectionFactory conn)
        {
            _connectionFactory = conn;
        }

        public int Add(ShoppingCartItem entity) // Create new shopping cart
        {
            var sql = "INSERT INTO ShoppingCartItems (ShoppingCartId,ProductId,Quantity) VALUES (@ShoppingCartId,@ProductId,@Quantity)";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public ShoppingCartItem AddToCart(int shoppingCartId, int productId, int quantity) // Add items to cart
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                ProductRepository _productRepository = new ProductRepository(_connectionFactory);
                var item = _productRepository.GetById(productId);
                var productItems = GetProducts(shoppingCartId).Find(x => x.ProductId == productId);

                var shopItem = new ShoppingCartItem()
                {
                    ShoppingCartId = shoppingCartId,
                    ProductId = productId,
                    Quantity = quantity
                };

                if (item.Quantity >= quantity)
                {
                    if (productItems != null)
                    {
                        var test = Update(shopItem);
                    }
                    else
                    {
                        var test = Add(shopItem);
                    }
                }

                return shopItem;
            }
        }

        public int ClearCart(int shoppingCartId) // Remove all items from cart
        {
            var sql = "DELETE FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, new { ShoppingCartId = shoppingCartId });
                return result;
            }
        }

        public List<ShoppingCartItem> GetProducts(int shoppingCartId)
        {
            var sql = "SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<ShoppingCartItem>(sql, new { ShoppingCartId = shoppingCartId }).ToList();
                return result;
            }
        }

        public decimal GetTotal(int shoppingCartId) // Get total price from shopping cart
        {
            var sql = "SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<ShoppingCartItem>(sql, new { ShoppingCartId = shoppingCartId }).ToList();

                decimal total = 0;
                var productRepository = new ProductRepository(_connectionFactory); // Connect to product repository to get products
                foreach (var item in result)
                {
                    item.Product = productRepository.GetById(item.ProductId); // Get product by id
                    total += item.Product.Price * item.Quantity; // Add the quantity of the current item to the total
                }

                return total;
            }
        }

        public void Ordered(int shoppingCartId) // Order items from shopping cart
        {
            var sql = "DELETE FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                connection.Execute(sql, new { ShoppingCartId = shoppingCartId });
            }
        }

        public int Update(ShoppingCartItem entity) // Update shopping cart
        {
            var sql = "UPDATE ShoppingCartItems SET ShoppingCartId = @ShoppingCartId, ProductId = @ProductId, Quantity = @Quantity" +
                " WHERE ShoppingCartId = @ShoppingCartId AND ProductId = @ProductId";

            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }
    }
}
