using CKK.Logic.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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
        private void BackClick(object sender, EventArgs args)
        {
            this.Close();
        }

        public void UpdateValues(Product product, int quantity)
        {
            itemName.Content = $"Name: {product.Name}";
            itemId.Content = $"ID: {product.Id}";
            itemPrice.Content = $"Price: {product.Price}";
            itemQuantity.Content = $"Quantity: {quantity}";
        }
    }
}
