using CKK.Persistance.Interfaces;
using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using CKK.Logic.Exceptions;

namespace CKK.Persistance.Models
{
    public class FileStore : IStore, ISavable, ILoadable
    {
        private List<StoreItem> items = new List<StoreItem>();
        public readonly string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Persistance" + Path.DirectorySeparatorChar + "StoreItems.dat";
        private int idCounter;

        public FileStore()
        {
            CreatePath();
            Load();
        }

        public StoreItem AddStoreItem(Product product, int quantity) // Uses idCounter unlike Store.cs
        {
            if (quantity < 1)
            {
                throw new InventoryItemStockTooLowException();
            }
            // Attempt to find an item with the same product
            var filteredProducts =
                from item in items
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
                            from itemid in items
                            select itemid.Product.Id;
                        idCounter = 1;
                        foreach (var id in filteredIds)
                        {
                            if (id == idCounter)
                            {
                                idCounter++;
                            }
                            else
                            {
                                item.Product.Id = idCounter;
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
                    from itemid in items
                    select itemid.Product.Id;
                idCounter = 1;
                if (filteredIds.Any())
                {
                    foreach (var id in filteredIds)
                    {
                        if (id == idCounter)
                        {
                            idCounter++;
                        }
                    }
                }
                newItem.Product.Id = idCounter;
                items.Add(newItem);
                return newItem;
            }
            return null;
        }

        public StoreItem RemoveStoreItem(int id, int quantity) // Identical to Store.cs
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            // Attempt to find an item with the same id
            var filteredProducts =
                from item in items
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

        public void DeleteStoreItem(int id) // Identical to Store.cs
        {
            var filteredProducts =
                from item in items
                where item.Product.Id == id
                select item;
            if (filteredProducts.Any())
            {
                foreach (var item in filteredProducts)
                {
                    items.Remove(item);
                }
            }
            else
            {
                throw new ProductDoesNotExistException();
            }
        }

        public StoreItem FindStoreItemById(int id) // Identical to Store.cs
        {
            if (id < 0)
            {
                throw new InvalidIdException();
            }
            var filteredProducts =
                from item in items
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

        public List<StoreItem> GetStoreItems() // Identical to Store.cs
        {
            return items;
        }

        public void Load()
        {
            if (File.Exists(filePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
                items.Clear();
                items = (List<StoreItem>)formatter.Deserialize(stream);
                stream.Close();
            }
        }

        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, items);
            stream.Close();
        }

        private void CreatePath()
        {
            string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Persistance";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); 
            }
        }
    }
}
