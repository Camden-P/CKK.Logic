using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Interfaces
{
    internal interface IShoppingCart
    {
        int GetCustomerId();
        ShoppingCartItem AddProduct(Product product, int quantity);
        ShoppingCartItem RemoveProduct(int id, int quantity);
        decimal GetTotal();
        ShoppingCartItem GetProductById(int id);
        List<ShoppingCartItem> GetProducts();
    }
}
