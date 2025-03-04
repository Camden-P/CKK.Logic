using CKK.Logic.Exceptions;

namespace CKK.Logic.Models
{
    // An product added by the customer for the Shopping Cart
    public class ShoppingCartItem
    {  
        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        private int quantity { get; set; }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                // If the quantity is less than 0, throw an exception
                if (value >= 0)
                {
                    quantity = value;
                }
                else
                {
                    throw new InventoryItemStockTooLowException();
                }
            }
        }

        public decimal GetTotal() // Get the total price of a product depending on the quantity
        {
            return Product.Price * Quantity;
        }
    }
}
