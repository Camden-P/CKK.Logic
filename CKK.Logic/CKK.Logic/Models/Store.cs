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
    }
}
