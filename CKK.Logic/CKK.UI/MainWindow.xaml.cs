﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Persistance.Models;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileStore Store;
        public ObservableCollection<StoreItem> _Items { get; private set; }
        public ObservableCollection<Product> _Products { get; private set; }
        private string[] sorts = { "ID", "Quantity", "Price" };
        EditStoreItem editItem = new EditStoreItem();

        public MainWindow()
        {
            Store = new FileStore();
            InitializeComponent();
            _Items = new ObservableCollection<StoreItem>();
            sortBox.ItemsSource = sorts;
            inventoryList.ItemsSource = _Items;
            this.DataContext = editItem;
            UpdateList();
        }
        private void AddItemClick(object sender, EventArgs args)
        {
            var product = new Product();
            product.Name = editItem.AddProduct;

            var products =
                from item in _Items
                where item.Product.Name == product.Name
                select item;
            if (products.Any())
            {
                foreach(var item in products)
                {
                    Store.AddStoreItem(item.Product, editItem.AddQuantity);
                }
            }
            else
            {
                Store.AddStoreItem(product, editItem.AddQuantity);
            }
            Store.Save();
            UpdateList();
        }
        private void RemoveItemClick(object sender, EventArgs args)
        {
            int productId = editItem.RemoveID;
            Product product = Store.FindStoreItemById(productId).Product;
            Store.RemoveStoreItem(product.Id, editItem.RemoveQuantity);
            Store.Save();
            UpdateList();
        }
        private void ChangeQuantityClick(object sender, EventArgs args)
        {
            int productId = editItem.QuantityID;
            Store.FindStoreItemById(productId).Quantity = editItem.QuantityValue;
            Store.Save();
        }
        private void ChangeNameClick(object sender, EventArgs args)
        {
            int productId = editItem.NameID;
            Store.FindStoreItemById(productId).Product.Name = editItem.NameValue;
            Store.Save();
            UpdateList();
        }
        private void ChangePriceClick(object sender, EventArgs args)
        {
            int productId = editItem.PriceID;
            Store.FindStoreItemById(productId).Product.Price = editItem.PriceValue;
            Store.Save();
            UpdateList();
        }

        private void SelectedItemChanged(object sender, EventArgs args)
        {
            int selectedItemIndex = inventoryList.SelectedIndex;
            SelectedItem selectedItemWindow = new SelectedItem();
            if (selectedItemIndex >= 0)
            {
                selectedItemWindow.Show();
                selectedItemWindow.UpdateValues(_Items[selectedItemIndex].Product, _Items[selectedItemIndex].Quantity);
                inventoryList.SelectedIndex = -1;
            }
        }

        private void SearchTextChanged(object sender, EventArgs args)
        {
            UpdateList(Store.GetAllProductsByName(searchBox.Text));
        }

        private void SortBoxChanged(object sender, EventArgs args)
        {
            var option = sortBox.SelectedItem;

            if (option == "ID")
            {
                UpdateList(Store.GetProductsByID());
            }
            else if (option == "Quantity")
            {
                UpdateList(Store.GetProductsByQuantity());
            }
            else if (option == "Price")
            {
                UpdateList(Store.GetProductsByPrice());
            }
        }

        private void UpdateList()
        {
            _Items.Clear();
            foreach (StoreItem storeItem in new ObservableCollection<StoreItem>(Store.GetStoreItems()))
            {
                _Items.Add(storeItem);
            }
        }

        private void UpdateList(List<StoreItem> storeItems)
        {
            _Items.Clear();
            foreach (StoreItem storeItem in new ObservableCollection<StoreItem>(storeItems))
            {
                _Items.Add(storeItem);
            }
        }
    }
}
