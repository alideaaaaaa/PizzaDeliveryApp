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
    /// Interaction logic for WindowAngajat.xaml
    /// </summary>
    public partial class WindowAngajat : Window
    {
        public WindowAngajat()
        {
            InitializeComponent();
       

            MainFrame.Navigate(new Page1());

        }

        private void bttnAcasa_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page1());
        }

        private void MainFrame_Navigate(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Button_Click_Produse(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProduseAngajat());
        }

        private void Button_Click_1_Comenzi(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ComenziPage());
        }

        private void Button_Click_3_Cont(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageCont());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientiPage());
        }
    }
}

