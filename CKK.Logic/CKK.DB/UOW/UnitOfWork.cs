using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using CKK.DB.Interfaces;
using CKK.DB.Repositories;

namespace CKK.DB.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IConnectionFactory conn)
        {
            Products = new ProductRepository(conn);
            Orders = new OrderRepository(conn);
            ShoppingCarts = new ShoppingCartRepository(conn);
        }

        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; set; }
    }
}
