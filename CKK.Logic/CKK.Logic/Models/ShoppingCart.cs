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
        private ShoppingCartItem _product1;
        private ShoppingCartItem _product2;
        private ShoppingCartItem _product3;

        // Methods
        public ShoppingCart(Customer customer)
        {
            _customer = customer;
        }
        public int GetCustomerId()
        {
            return _customer.GetId();
        }
        public ShoppingCartItem GetProductById(int id)
        {
            if (_product1.GetProduct().GetId() == id)
            {
                return _product1;
            }
            else if (_product2.GetProduct().GetId() == id)
            {
                return _product2;
            }
            else if (_product3.GetProduct().GetId() == id)
            {
                return _product3;
            }
            else
            {
                return null;
            }
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
            if (_product1 != null && _product1.GetProduct().GetId() == product.GetId())
            {
                _product1.SetQuantity(_product1.GetQuantity() + quantity);
                return _product1;
            }
            else if (_product2 != null && _product2.GetProduct().GetId() == product.GetId())
            {
                _product2.SetQuantity(_product2.GetQuantity() + quantity);
                return _product2;
            }
            else if (_product3 != null && _product3.GetProduct().GetId() == product.GetId())
            {
                _product3.SetQuantity(_product3.GetQuantity() + quantity);
                return _product3;
            }
            // If it doesn't exist, check for a spot open
            if (_product1 == null)
            {
                _product1 = new ShoppingCartItem(product, quantity);
                return _product1;
            }
            else if (_product2 == null)
            {
                _product2 = new ShoppingCartItem(product, quantity);
                return _product2;
            }
            else if (_product3 == null)
            {
                _product3 = new ShoppingCartItem(product, quantity);
                return _product3;
            }
            else
            {
                return null;
            }
        }
        public ShoppingCartItem RemoveProduct(Product product, int quantity)
        {
            if (quantity < 1)
            {
                return null;
            }
            if (_product1 != null && _product1.GetProduct().GetId() == product.GetId())
            {
                _product1.SetQuantity(_product1.GetQuantity() - quantity);
                if (_product1.GetQuantity() < 1)
                {
                    return null;
                }
                return _product1;
            }
            else if (_product2 != null && _product2.GetProduct().GetId() == product.GetId())
            {
                _product2.SetQuantity(_product2.GetQuantity() - quantity);
                if (_product2.GetQuantity() < 1)
                {
                    return null;
                }
                return _product2;
            }
            else if (_product3 != null && _product3.GetProduct().GetId() == product.GetId())
            {
                _product3.SetQuantity(_product3.GetQuantity() - quantity);
                if (_product3.GetQuantity() < 1)
                {
                    return null;
                }
                return _product3;
            }
            else
            {
                return null;
            }
        }
        public decimal GetTotal()
        {
            var grandTotal = 0m;
            if (_product1 != null)
            {
                grandTotal += _product1.GetTotal();
            }
            if (_product2 != null)
            {
                grandTotal += _product2.GetTotal();
            }
            if (_product3 != null)
            {
                grandTotal += _product3.GetTotal();
            }
            return grandTotal;
        }
        public ShoppingCartItem GetProduct(int productNumber)
        {
            if (productNumber == 1)
            {
                return _product1;
            }
            else if (productNumber == 2)
            {
                return _product2;
            }
            else if (productNumber == 3)
            {
                return _product3;
            }
            else
            {
                return null;
            }
        }
    }
}
