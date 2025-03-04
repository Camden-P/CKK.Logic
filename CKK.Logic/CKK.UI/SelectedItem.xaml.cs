using CKK.Logic.Models;
using System;
using System.Windows;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for SelectedItem.xaml
    /// </summary>
    public partial class SelectedItem : Window
    {
        public SelectedItem()
        {
            InitializeComponent();
        }

        // Close window when user clicks the back button
        private void BackClick(object sender, EventArgs args)
        {
            this.Close();
        }

        // Populate values using data sent from the main window
        public void UpdateValues(Product product)
        {
            itemName.Content = $"Name: {product.Name}";
            itemId.Content = $"ID: {product.Id}";
            itemPrice.Content = $"Price: {product.Price}";
            itemQuantity.Content = $"Quantity: {product.Quantity}";
        }
    }
}
