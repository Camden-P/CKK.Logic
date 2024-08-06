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
        private Product _product1;
        private Product _product2;
        private Product _product3;

        // Methods
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
        public void AddStoreItem(Product product)
        {
            if(_product1 == null)
            {
                _product1 = product;
            }
            else if (_product2 == null)
            {
                _product2 = product;
            }
            else if (_product3 == null)
            {
                _product3 = product;
            }
        }
        public void RemoveStoreItem(int productNumber)
        {
            if (productNumber == 1)
            {
                _product1 = null;
            }
            else if (productNumber == 2)
            {
                _product2 = null;
            }
            else if (productNumber == 3)
            {
                _product3 = null;
            }
        }
        public Product GetStoreItem(int productNumber)
        {
            if (productNumber == 1)
            {
                return _product1;
            }
            else if (productNumber == 2)
            {
                return _product2;
            }
            else if (productNumber == 3)
            {
                return _product3;
            }
            else
            {
                return null;
            }
        }

        public Product FindStoreItemById(int id)
        {
            if (_product1.GetId() == id)
            {
                return _product1;
            }
            else if (_product2.GetId() == id)
            {
                return _product2;
            }
            else if (_product3.GetId() == id)
            {
                return _product3;
            }
            else
            {
                return null;
            }
        }
    }
}
