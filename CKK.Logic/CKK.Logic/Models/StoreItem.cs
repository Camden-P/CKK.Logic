﻿using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    [Serializable]
    public class StoreItem : InventoryItem
    {
        // Methods
        public StoreItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
