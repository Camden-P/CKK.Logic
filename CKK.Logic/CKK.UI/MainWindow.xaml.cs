using System;
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

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IStore Store;
        public ObservableCollection<StoreItem> _Items { get; private set; }
        public ObservableCollection<Product> _Products { get; private set; }
        EditStoreItem editItem = new EditStoreItem();

        public MainWindow()
        {
            Store = new Store();
            InitializeComponent();
            _Items = new ObservableCollection<StoreItem>();
            _Products = new ObservableCollection<Product>();
            inventoryList.ItemsSource = _Products;
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
            UpdateList();
        }
        private void RemoveItemClick(object sender, EventArgs args)
        {
            int productId = editItem.RemoveID;
            Product product = Store.FindStoreItemById(productId).Product;
            Store.RemoveStoreItem(product.Id, editItem.RemoveQuantity);
            UpdateList();
        }
        private void ChangeQuantityClick(object sender, EventArgs args)
        {
            int productId = editItem.QuantityID;
            Store.FindStoreItemById(productId).Quantity = editItem.QuantityValue;
        }
        private void ChangeNameClick(object sender, EventArgs args)
        {
            int productId = editItem.NameID;
            Store.FindStoreItemById(productId).Product.Name = editItem.NameValue;
            UpdateList();
        }
        private void ChangePriceClick(object sender, EventArgs args)
        {
            int productId = editItem.PriceID;
            Store.FindStoreItemById(productId).Product.Price = editItem.PriceValue;
            UpdateList();
        }

        private void SelectedItemChanged(object sender, EventArgs args)
        {
            int selectedItemIndex = inventoryList.SelectedIndex;
            SelectedItem selectedItemWindow = new SelectedItem();
            if (selectedItemIndex >= 0)
            {
                selectedItemWindow.Show();
                selectedItemWindow.UpdateValues(_Products[selectedItemIndex], _Items[selectedItemIndex].Quantity);
                inventoryList.SelectedIndex = -1;
            }
        }

        private void UpdateList()
        {
            _Items.Clear();
            _Products.Clear();
            foreach (StoreItem storeItem in new ObservableCollection<StoreItem>(Store.GetStoreItems()))
            {
                _Items.Add(storeItem);
                _Products.Add(storeItem.Product);
            }
        }
    }
}
