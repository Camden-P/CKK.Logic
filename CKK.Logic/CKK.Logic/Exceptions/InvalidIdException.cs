namespace CKK.Logic.Exceptions
{
    // If there was an ID transferred for a product is invalid, this exception will be thrown
    [Serializable]
    public class InvalidIdException : Exception
    {
        public InvalidIdException() : base("Invalid Id.") { }
        public InvalidIdException(string message) : base(message) { }
        public InvalidIdException(string message, Exception innerException) : base(message, innerException) { }
    }
}
