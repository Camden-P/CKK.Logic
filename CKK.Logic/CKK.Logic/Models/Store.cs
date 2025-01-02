using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Store : Entity, IStore
    {
        // Instance Variables
        private List<StoreItem> _items;

        // Methods
        public Store()
        {
            _items = new List<StoreItem>();
        }

        public StoreItem AddStoreItem(Product product, int quantity)
        {
            if (quantity < 1)
            {
                throw new InventoryItemStockTooLowException();
            }
            // Attempt to find an item with the same product
            var filteredProducts =
                from item in _items
                where item.Product == product
                select item;
            // If there is an item found, change the quantity, else add a new item
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    if (item.Product.Id == 0)
                    {
                        var filteredIds =
                            from itemid in _items
                            select itemid.Product.Id;
                        int newId = 1;
                        foreach (var id in filteredIds)
                        {
                            if (id == newId)
                            {
                                newId++;
                            }
                            else
                            {
                                item.Product.Id = newId;
                            }
                        }
                    }
                    item.Quantity += quantity;
                    return item;
                }
            }
            else
            {
                var newItem = new StoreItem(product, quantity);
                var filteredIds =
                    from itemid in _items
                    select itemid.Product.Id;
                int newId = 1;
                if (filteredIds.Any())
                {
                    foreach (var id in filteredIds)
                    {
                        if (id == newId)
                        {
                            newId++;
                        }
                    }
                }
                newItem.Product.Id = newId;
                _items.Add(newItem);
                return newItem;
            }
            return null;
        }
        public StoreItem RemoveStoreItem(int id, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            // Attempt to find an item with the same id
            var filteredProducts =
                from item in _items
                where item.Product.Id == id
                select item;
            // If there is an item found, check for if the count is above 0, if it is, either remove a specific quantity, or the item altogether
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    if (item.Quantity > quantity)
                    {
                        item.Quantity -= quantity;
                        return item;
                    }
                    else
                    {
                        item.Quantity = 0;
                        return item;
                    }
                }
            }
            else
            {
                throw new ProductDoesNotExistException();
            }
            return null;
        }

        public void DeleteStoreItem(int id)
        {
            var filteredProducts =
                from item in _items
                where item.Product.Id == id
                select item;
            if (filteredProducts.Any())
            {
                foreach(var item in filteredProducts)
                {
                    _items.Remove(item);
                }
            }
            else
            {
                throw new ProductDoesNotExistException();
            }
        }

        public List<StoreItem> GetStoreItems()
        {
            return _items;
        }

        public StoreItem FindStoreItemById(int id)
        {
            if (id < 0)
            {
                throw new InvalidIdException();
            }
            var filteredProducts =
                from item in _items
                where item.Product.Id == id
                select item;
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    return item;
                }
            }
            return null;
        }

        public List<StoreItem> GetAllProductsByName(string name)
        {
            // Create list for search
            List<StoreItem> products = new List<StoreItem>();
            
            // Check every item in the store
            foreach(var item in _items)
            {
                bool validProduct = true;

                // Test if each character matches the search
                for (int i = 0; i < name.Length; i++)
                {
                    if (i < item.Product.Name.Length)
                    {
                        if (item.Product.Name[i] != name[i])
                        {
                            validProduct = false;
                        }
                    }
                    else
                    {
                        validProduct = false;
                    }
                }

                // If it matches, add the product for the search list
                if (validProduct)
                {
                    products.Add(item);
                }
            }

            return products;
        }

        public List<StoreItem> GetProductsByQuantity()
        {
            // Get items to sort
            List<StoreItem> sortedItems = new List<StoreItem>();
            foreach (var item in _items)
            {
                sortedItems.Add(item);
            }

            // Sort from largest to smallest in quantities
            for (int i = 0; i < sortedItems.Count - 1; ++i)
            {
                for (int j = i + 1; j < sortedItems.Count; ++j)
                {
                    if (sortedItems[j].Quantity > sortedItems[i].Quantity)
                    {
                        SwapItems(ref sortedItems, i, j);
                    }
                }
            }

            return sortedItems;
        }

        public List<StoreItem> GetProductsByPrice()
        {
            // Get items to sort
            List<StoreItem> sortedItems = new List<StoreItem>();
            foreach (var item in _items)
            {
                sortedItems.Add(item);
            }

            // Sort from largest to smallest in prices
            for (int i = 0; i < sortedItems.Count - 1; ++i)
            {
                for (int j = i + 1; j < sortedItems.Count; ++j)
                {
                    if (sortedItems[j].Product.Price > sortedItems[i].Product.Price)
                    {
                        SwapItems(ref sortedItems, i, j);
                    }
                }
            }

            return sortedItems;
        }

        public List<StoreItem> GetProductsByID()
        {
            // Get items to sort
            List<StoreItem> sortedItems = new List<StoreItem>();
            foreach (var item in _items)
            {
                sortedItems.Add(item);
            }

            // Sort from smallest to largest in ids
            for (int i = 0; i < sortedItems.Count - 1; ++i)
            {
                for (int j = i + 1; j < sortedItems.Count; ++j)
                {
                    if (sortedItems[j].Product.Id < sortedItems[i].Product.Id)
                    {
                        SwapItems(ref sortedItems, i, j);
                    }
                }
            }

            return sortedItems;
        }

        private void SwapItems(ref List<StoreItem> swapItems, int first, int second)
        {
            // Swap two items with each other
            var temp = swapItems[first];
            swapItems[first] = swapItems[second];
            swapItems[second] = temp;
        }
    }
}
