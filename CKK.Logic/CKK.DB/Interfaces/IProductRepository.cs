using CKK.Logic.Models;

namespace CKK.DB.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product> // Repository for products
    {
        // Method to be included in the repository
        List<Product> GetByName(string name);
    }
}
