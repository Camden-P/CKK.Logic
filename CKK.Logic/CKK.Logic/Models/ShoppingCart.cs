using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCart
    {
        // Instance Variables
        private Customer _customer;
        private List<ShoppingCartItem> _products;

        // Methods
        public ShoppingCart(Customer customer)
        {
            _customer = customer;
            _products = new List<ShoppingCartItem>();
        }
        public int GetCustomerId()
        {
            return _customer.GetId();
        }
        public ShoppingCartItem GetProductById(int id)
        {
            // Check if there are products that match the Id, select any that match
            var filteredProducts =
                from item in _products
                where item.GetProduct().GetId() == id
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
                from item in _products
                where item.GetProduct() == product
                select item;
            // If there is an item found, change the quantity, else add an item
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    item.SetQuantity(item.GetQuantity() + quantity);
                    return item;
                }
            }
            else
            {
                var newItem = new ShoppingCartItem(product, quantity);
                _products.Add(newItem);
                return newItem;
            }
            return null;
        }
        public ShoppingCartItem RemoveProduct(Product product, int quantity)
        {
            if (quantity < 1)
            {
                return null;
            }
            var filteredProducts =
                from item in _products
                where item.GetProduct() == product
                select item;
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    int itemQuantity = item.GetQuantity();
                    if (itemQuantity > quantity)
                    {
                        item.SetQuantity(itemQuantity - quantity);
                        return item;
                    }
                    else
                    {
                        item.SetQuantity(0);
                        _products.Remove(item);
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
                from product in _products
                select product;
            foreach (var product in products)
            {
                grandTotal += product.GetTotal();
            }
            return grandTotal;
        }
        public List<ShoppingCartItem> GetProducts()
        {
            return _products;
        }
    }
}
