using CKK.Logic.Models;

namespace CKK.tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddingProduct()
        {
            // Assemble
            Customer customer = new Customer();
            Product expected = new Product();
            ShoppingCart cart = new ShoppingCart(customer);

            expected.SetId(1);
            expected.SetName("Apple");
            expected.SetPrice(0.99M);

            // Act
            ShoppingCartItem actual = cart.AddProduct(expected);

            // Assert
            Assert.Equal(expected, actual.GetProduct());
        }

        [Fact]
        public void RemovingProduct()
        {
            // Assemble
            Customer customer = new Customer();
            ShoppingCart cart = new ShoppingCart(customer);

            Product product = new Product();
            product.SetId(1);
            product.SetName("Apple");
            product.SetPrice(0.99M);

            ShoppingCartItem expected = new ShoppingCartItem(product, 1);
            expected = null;

            // Act
            cart.AddProduct(product);
            ShoppingCartItem actual = cart.RemoveProduct(product, 1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GettingTotal()
        {
            // Assemble
            Customer customer = new Customer();
            Product product1 = new Product();
            Product product2 = new Product();
            Product product3 = new Product();
            ShoppingCart cart = new ShoppingCart(customer);

            product1.SetId(1);
            product1.SetName("Beans");
            product1.SetPrice(2.99M);

            product2.SetId(2);
            product2.SetName("Apple");
            product2.SetPrice(0.99M);

            product3.SetId(3);
            product3.SetName("Sandwich");
            product3.SetPrice(4.99M);

            decimal expected = product1.GetPrice() + product2.GetPrice() + product3.GetPrice();

            // Act
            cart.AddProduct(product1);
            cart.AddProduct(product2);
            cart.AddProduct(product3);

            decimal actual = cart.GetTotal();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}