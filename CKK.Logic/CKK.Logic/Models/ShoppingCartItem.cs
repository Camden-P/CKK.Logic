using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCartItem : InventoryItem
    {  
        // Methods
        public ShoppingCartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal GetTotal()
        {
            return Product.Price * Quantity;
        }
    }
}
