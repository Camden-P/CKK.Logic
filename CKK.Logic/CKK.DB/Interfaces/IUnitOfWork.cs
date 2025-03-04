namespace CKK.DB.Interfaces
{
    public interface IUnitOfWork // Interface for Unit of Work
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IShoppingCartRepository ShoppingCarts { get; }
    }
}
