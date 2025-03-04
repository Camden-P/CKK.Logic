namespace CKK.Logic.Exceptions
{
    // If the stock is below 0, this exception will be thrown
    [Serializable]
    public class InventoryItemStockTooLowException : Exception
    {
        public InventoryItemStockTooLowException() : base("Inventory Item Stock Too Low.") { }
        public InventoryItemStockTooLowException(string message) : base(message) { }
        public InventoryItemStockTooLowException(string message, Exception innerException) : base(message, innerException) { }
    }
}
