using PizzaDeliveySistem.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaDeliveySistem
{
    /// <summary>
    /// Interaction logic for ComenziGeneralePage.xaml
    /// </summary>
    public partial class ComenziGeneralePage : Page
    {
        private readonly ComenziActive2 c = new ComenziActive2();
        private readonly AppDataContext dbContext = new AppDataContext();
        public ComenziGeneralePage()
        {
            InitializeComponent();
            dataGrid.ItemsSource = dbContext.Orders.ToList();
            comboBox.ItemsSource = new List<string> { "Crescator dupa suma", "Crescator dupa data", "Descrescator dupa data", "Descrescator dupa suma" };
            comboBox2.ItemsSource = new List<string> {"Plasata","Pregatire","Livrata","Finalizata","Anulata" }; 
        }
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)

        {
            string text = textBoxCautare.Text.ToLower();

            dataGrid.ItemsSource = dbContext.Orders.Where(p => p.IDClient.ToString().Contains(text)).ToList();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            c.Filtrare3(dateTime1, dateTime2, dataGrid);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dateTime2.SelectedDate = null;
            dateTime1.SelectedDate = null;
            comboBox.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dataGrid.ItemsSource = dbContext.Orders.ToList();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    {
                        dataGrid.ItemsSource = dbContext.Orders.OrderBy(o => o.Suma).ToList();
                        break;
                    }
                case 1:
                    {
                        dataGrid.ItemsSource = dbContext.Orders.OrderBy(o => o.DataComanda).ToList();
                        break;
                    }
                case 2:
                    {
                        dataGrid.ItemsSource = dbContext.Orders.OrderByDescending(o => o.DataComanda).ToList();
                        break;
                    }
                case 3:
                    {
                        dataGrid.ItemsSource = dbContext.Orders.OrderByDescending(o => o.Suma).ToList();
                        break;
                    }
               
            }
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = dbContext.Orders.Where(o => o.Status == (status)comboBox2.SelectedIndex).ToList();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Modificarile au fost salvate!");
            dbContext.SaveChanges();
         
        }
    }
}

