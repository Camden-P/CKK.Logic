namespace CKK.UI
{
    public class EditStoreItem
    {
        // Add item
        private string addProduct;
        public string AddProduct
        {
            get { return addProduct; }
            set { addProduct = value; }
        }
        private int addQuantity;
        public int AddQuantity
        {
            get { return addQuantity; }
            set { addQuantity = (int)value; }
        }

        // Remove item
        private int removeId;
        public int RemoveID
        {
            get { return removeId; }
            set { removeId = (int)value; }
        }
        private int removeQuantity;
        public int RemoveQuantity
        {
            get { return removeQuantity; }
            set { removeQuantity = (int)value; }
        }

        // Change quantity
        private int quantityId;
        public int QuantityID
        {
            get { return quantityId; }
            set { quantityId = (int)value; }
        }
        private int quantityValue;
        public int QuantityValue
        {
            get { return quantityValue; }
            set { quantityValue = (int)value; }
        }

        // Change name
        private int nameId;
        public int NameID
        {
            get { return nameId; }
            set { nameId = (int)value; }
        }
        private string nameValue;
        public string NameValue
        {
            get { return nameValue; }
            set { nameValue = value; }
        }

        // Change price
        private int priceId;
        public int PriceID
        {
            get { return priceId; }
            set { priceId = (int)value; }
        }
        private decimal priceValue;
        public decimal PriceValue
        {
            get { return priceValue; }
            set { priceValue = (decimal)value; }
        }
    }
}
