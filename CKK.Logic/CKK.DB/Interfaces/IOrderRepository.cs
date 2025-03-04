using CKK.Logic.Models;

namespace CKK.DB.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order> // Repository for orders
    {
        // Methods to be included in the repository
        Order GetOrderByCustomerId(int id);
    }
}
