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
using CKK.DB.Repositories;
using CKK.DB.UOW;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> _Items { get; private set; }
        private string[] sorts = { "ID", "Quantity", "Price" };
        EditStoreItem editItem = new EditStoreItem();
        private readonly DatabaseConnectionFactory db;
        private ProductRepository products;

        public MainWindow()
        {
            InitializeComponent();
            _Items = new ObservableCollection<Product>();
            sortBox.ItemsSource = sorts;
            inventoryList.ItemsSource = _Items;
            this.DataContext = editItem;
            db = new DatabaseConnectionFactory();
            products = new ProductRepository(db);
            UpdateList();
        }
        private void AddItemClick(object sender, EventArgs args)
        {
            // Create new temporary product
            var product = new Product();
            product.Name = editItem.AddProduct;

            // Attempt to find product of same name
            var searchedProducts =
                from item in _Items
                where item.Name == product.Name
                select item;
            // If product exists, just add more quantity, else create a new product
            if (searchedProducts.Any())
            {
                foreach(var item in searchedProducts)
                {
                    item.Quantity += editItem.AddQuantity;
                    products.Update(item);
                }
            }
            else
            {
                product.Quantity = editItem.AddQuantity;
                products.Add(product);
            }

            UpdateList();
        }
        private void RemoveItemClick(object sender, EventArgs args)
        {
            // If the quantity would be less than 0, set it to 0
            var product = products.GetById(editItem.RemoveID);
            if (product.Quantity - editItem.RemoveQuantity < 0)
            {
                product.Quantity = 0;
                products.Update(product);
            }
            else
            {
                product.Quantity -= editItem.RemoveQuantity;
                products.Update(product);
            }

            UpdateList();
        }
        
        // Change quantity of product
        private void ChangeQuantityClick(object sender, EventArgs args)
        {
            var product = products.GetById(editItem.QuantityID);
            product.Quantity = editItem.QuantityValue;
            products.Update(product);

            UpdateList();
        }

        // Change name of product
        private void ChangeNameClick(object sender, EventArgs args)
        {
            var product = products.GetById(editItem.NameID);
            product.Name = editItem.NameValue;
            products.Update(product);

            UpdateList();
        }

        // Change price of product
        private void ChangePriceClick(object sender, EventArgs args)
        {
            var product = products.GetById(editItem.PriceID);
            product.Price = editItem.PriceValue;
            products.Update(product);

            UpdateList();
        }

        // Open selected item window
        private void SelectedItemChanged(object sender, EventArgs args)
        {
            int selectedItemIndex = inventoryList.SelectedIndex;
            SelectedItem selectedItemWindow = new SelectedItem();
            if (selectedItemIndex >= 0)
            {
                selectedItemWindow.Show();
                selectedItemWindow.UpdateValues(_Items[selectedItemIndex]);
                inventoryList.SelectedIndex = -1;
            }
        }

        // Search for products
        private void SearchTextChanged(object sender, EventArgs args)
        {
            UpdateList(products.GetByName(searchBox.Text));
        }

        // Sort products by ID, Quantity, or Price
        private void SortBoxChanged(object sender, EventArgs args)
        {
            var option = sortBox.SelectedItem;

            if (option == "ID")
            {
                UpdateList(products.GetAllByID());
            }
            else if (option == "Quantity")
            {
                UpdateList(products.GetAllByQuantity());
            }
            else if (option == "Price")
            {
                UpdateList(products.GetAllByPrice());
            }
        }

        // Default list for listbox
        private void UpdateList()
        {
            UpdateList(products.GetAll());
        }

        // Update the listbox to see latest changes
        private void UpdateList(List<Product> list)
        {
            _Items.Clear();
            foreach (var item in list)
            {
                _Items.Add(item);
            }
        }
    }
}
