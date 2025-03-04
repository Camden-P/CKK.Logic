using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CKK.Logic.Exceptions;
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
        public ObservableCollection<Product> _Items { get; private set; } // List of items
        private string[] sorts = { "ID", "Quantity", "Price" }; // Different sorting types
        EditStoreItem editItem = new EditStoreItem(); // Methods to edit items
        private readonly DatabaseConnectionFactory db; // Database connection
        private ProductRepository products; // Products from database

        public MainWindow()
        {
            InitializeComponent();
            _Items = new ObservableCollection<Product>();
            sortBox.ItemsSource = sorts; // GUI connected to sorting types
            inventoryList.ItemsSource = _Items; // GUI connected to list of items
            this.DataContext = editItem; // Methods for data binding
            db = new DatabaseConnectionFactory(); // Database connection
            products = new ProductRepository(db); // Connect to the products table from the database
            UpdateList();
        }

        // Add item to store
        private void AddItemClick(object sender, EventArgs args)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(editItem.AddProduct)) // If the name is null, throw an exception
                {
                    throw new ArgumentNullException();
                }

                if (editItem.AddQuantity < 0) // If the quantity is less than 0, throw an exception
                {
                    throw new ArgumentOutOfRangeException();
                }

                // Create new temporary product
                var product = new Product();
                product.Name = editItem.AddProduct.Trim();

                // Attempt to find product of same name
                var searchedProducts =
                    from item in _Items
                    where item.Name == product.Name
                    select item;

                // If product exists, just add more quantity, else create a new product
                if (searchedProducts.Any())
                {
                    foreach (var item in searchedProducts)
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
            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }

            UpdateList();
        }

        // Remove item from store
        private void RemoveItemClick(object sender, EventArgs args)
        {
            var product = new Product();

            try
            {
                product = products.GetById(editItem.RemoveID); // Attempt to get product from ID

                if (product == null) // If the attempt fails, throw an exception
                {
                    throw new InvalidIdException();
                }

                if (editItem.RemoveQuantity < 0) // If the quantity is less than 0, throw an exception
                {
                    throw new ArgumentOutOfRangeException();
                }

                // If the quantity would be less than 0, set it to 0
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
            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }

            UpdateList();
        }
        
        // Change quantity of product
        private void ChangeQuantityClick(object sender, EventArgs args)
        {
            var product = new Product();
            try
            {
                product = products.GetById(editItem.QuantityID); // Attempt to get product from ID

                if (product == null) // If the attempt fails, throw an exception
                {
                    throw new InvalidIdException();
                }
                
                if (editItem.QuantityValue < 0) // If the quantity is less than 0, throw an exception
                {
                    throw new ArgumentOutOfRangeException();
                }

                product.Quantity = editItem.QuantityValue;
                products.Update(product);
            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }

            UpdateList();
        }

        // Change name of product
        private void ChangeNameClick(object sender, EventArgs args)
        {
            var product = new Product();
            try
            {
                product = products.GetById(editItem.NameID); // Attempt to get product from ID

                if (product == null) // If the attempt fails, throw an exception
                {
                    throw new InvalidIdException();
                }

                if (String.IsNullOrWhiteSpace(editItem.NameValue)) // If the name is null, throw an exception
                {
                    throw new ArgumentNullException();
                }

                product.Name = editItem.NameValue.Trim();
                products.Update(product);
            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }

            UpdateList();
        }

        // Change price of product
        private void ChangePriceClick(object sender, EventArgs args)
        {
            var product = new Product();
            try
            {
                product = products.GetById(editItem.PriceID); // Attempt to get product from ID

                if (product == null) // If the attempt fails, throw an exception
                {
                    throw new InvalidIdException();
                }

                if (editItem.PriceValue < 0) // If the price is below 0, throw an exception
                {
                    throw new ArgumentOutOfRangeException();
                }

                product.Price = editItem.PriceValue;
                products.Update(product);
            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }

            UpdateList();
        }

        // Display error for exceptions
        private void DisplayError(string message)
        {
            var error = MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Open selected item window
        private void SelectedItemChanged(object sender, EventArgs args)
        {
            int selectedItemIndex = inventoryList.SelectedIndex; // Get the item selected

            if (selectedItemIndex >= 0) // If an item is selected...
            {
                // Create and open new window
                SelectedItem selectedItemWindow = new SelectedItem();
                selectedItemWindow.Show();
                selectedItemWindow.UpdateValues(_Items[selectedItemIndex]); // Populate values in the new window
                inventoryList.SelectedIndex = -1; // Deselect the item
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
            string option = (string)sortBox.SelectedItem; // Get the option selected

            // Decide how to sort based on option
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

        // Default list for the list of products
        private void UpdateList()
        {
            UpdateList(products.GetAll());
        }

        // Update the list of products to see latest changes
        private void UpdateList(List<Product> list)
        {
            _Items.Clear();
            foreach (var item in list)
            {
                _Items.Add(item);
            }
        }

        // If the window is closed, shutdown the entire application
        protected override void OnClosed(EventArgs args)
        {
            Application.Current.Shutdown();
        }
    }
}
