using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PizzaDeliveySistem
{
    /// <summary>
    /// Interaction logic for Plata.xaml
    /// </summary>
    public partial class Plata : Window
    {

        public Plata()
        {
            InitializeComponent();
        }

        private void ButtonConfirma_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCard.Text) == true )
            {
                MessageBox.Show("Introduceti numarul cardului!");
            }
            else {
                this.DialogResult =true;
                this.Close();
            }
        }
    }
}
