using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCart
    {
        // Properties
        public Customer Customer { get; set; }
        public List<ShoppingCartItem> Products { get; set; }

        // Methods
        public ShoppingCart(Customer customer)
        {
            Customer = customer;
            Products = new List<ShoppingCartItem>();
        }
        public int GetCustomerId()
        {
            return Customer.Id;
        }
        public ShoppingCartItem GetProductById(int id)
        {
            // Check if there are products that match the Id, select any that match
            var filteredProducts =
                from item in Products
                where item.Product.Id == id
                select item;
            // If there are any products that match, return the product
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    return item;
                }
            }
            return null;
        }
        public ShoppingCartItem AddProduct(Product product)
        {
            return AddProduct(product, 1);
        }
        public ShoppingCartItem AddProduct(Product product, int quantity)
        {
            if (quantity < 1)
            {
                return null;
            }
            // Attempt to find item with same product
            var filteredProducts =
                from item in Products
                where item.Product == product
                select item;
            // If there is an item found, change the quantity, else add an item
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    item.Quantity += quantity;
                    return item;
                }
            }
            else
            {
                var newItem = new ShoppingCartItem(product, quantity);
                Products.Add(newItem);
                return newItem;
            }
            return null;
        }
        public ShoppingCartItem RemoveProduct(int id, int quantity)
        {
            if (quantity < 1)
            {
                return null;
            }
            var filteredProducts =
                from item in Products
                where item.Product.Id == id
                select item;
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    if (item.Quantity > quantity)
                    {
                        item.Quantity -= quantity;
                        return item;
                    }
                    else
                    {
                        item.Quantity = 0;
                        Products.Remove(item);
                        return item;
                    }
                }
            }
            return null;
        }
        public decimal GetTotal()
        {
            var grandTotal = 0m;
            var products =
                from product in Products
                select product;
            foreach (var product in products)
            {
                grandTotal += product.GetTotal();
            }
            return grandTotal;
        }
        public List<ShoppingCartItem> GetProducts()
        {
            return Products;
        }
    }
}
