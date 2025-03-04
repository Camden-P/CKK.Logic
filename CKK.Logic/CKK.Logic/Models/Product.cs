namespace CKK.Logic.Models
{
    // Properties of a store item
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public override string ToString() // Override for UI to display list of products
        {
            return $"ID: {Id} | Name: {Name}";
        }
    }
}
