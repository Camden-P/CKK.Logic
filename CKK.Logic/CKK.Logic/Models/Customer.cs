namespace CKK.Logic.Models
{
    // A person who orders products from the store
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
