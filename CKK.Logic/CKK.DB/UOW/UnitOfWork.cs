using CKK.DB.Interfaces;
using CKK.DB.Repositories;

namespace CKK.DB.UOW
{
    public class UnitOfWork : IUnitOfWork // Unit of Work for Products, Orders, and Shopping Carts
    {
        public UnitOfWork(IConnectionFactory conn) // Constructor
        {
            Products = new ProductRepository(conn);
            Orders = new OrderRepository(conn);
            ShoppingCarts = new ShoppingCartRepository(conn);
        }

        // Properties
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; set; }
    }
}
