namespace CKK.Logic.Exceptions
{
    // If a product does not exist, this exception will be thrown
    [Serializable]
    public class ProductDoesNotExistException : Exception
    {
        public ProductDoesNotExistException() : base("Product does not exist.") { }
        public ProductDoesNotExistException(string message) : base(message) { }
        public ProductDoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
    }
}
