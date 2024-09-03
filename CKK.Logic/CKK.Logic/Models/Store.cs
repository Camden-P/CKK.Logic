using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Store
    {
        // Instance Variables
        private int _id;
        private string _name;
        private List<StoreItem> _items;

        // Methods
        public Store()
        {
            _items = new List<StoreItem>();
        }
        public int GetId()
        {
            return _id;
        }
        public void SetId(int id)
        {
            _id = id;
        }
        public string GetName()
        {
            return _name;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public StoreItem AddStoreItem(Product product, int quantity)
        {
            if (quantity < 1)
            {
                return null;
            }
            // Attempt to find an item with the same product
            var filteredProducts =
                from item in _items
                where item.GetProduct() == product
                select item;
            // If there is an item found, change the quantity, else add a new item
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    item.SetQuantity(item.GetQuantity() + quantity);
                    return item;
                }
            }
            else
            {
                var newItem = new StoreItem(product, quantity);
                _items.Add(newItem);
                return newItem;
            }
            return null;
        }
        public StoreItem RemoveStoreItem(int id, int quantity)
        {
            if (quantity < 1)
            {
                return null;
            }
            // Attempt to find an item with the same id
            var filteredProducts =
                from item in _items
                where item.GetProduct().GetId() == id
                select item;
            // If there is an item found, check for if the count is above 0, if it is, either remove a specific quantity, or the item altogether
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    int itemQuantity = item.GetQuantity();
                    if (itemQuantity > quantity)
                    {
                        item.SetQuantity(itemQuantity - quantity);
                        return item;
                    }
                    else
                    {
                        item.SetQuantity(0);
                        return item;
                    }
                }
            }
            return null;
        }
        public List<StoreItem> GetStoreItems()
        {
            return _items;
        }

        public StoreItem FindStoreItemById(int id)
        {
            var filteredProducts =
                from item in _items
                where item.GetProduct().GetId() == id
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
