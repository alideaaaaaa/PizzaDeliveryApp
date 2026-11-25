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
    /// Interaction logic for PageProduse.xaml
    /// </summary>
    public partial class PageProduse : Page
    {
        private readonly AppDataContext dbContext = new AppDataContext();
        public PageProduse()
        {
            InitializeComponent();
            dataGridProduse.ItemsSource = dbContext.Pizzass.ToList();
            comboBox.ItemsSource = new List<string> { "Crescator dupa nume", "Crescator dupa pret",
                "Crescator dupa cantitatea ramasa","Descrescator dupa nume", "Descrescator dupa pret","Descrescator dupa cantitatea ramasa"};

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var prod = dataGridProduse.SelectedItem as Pizzas;

            if (prod != null)
            {
                dbContext.Pizzass.Remove(prod);
                dbContext.SaveChanges();
                dataGridProduse.ItemsSource=dbContext.Pizzass.ToList();
                MessageBox.Show("Produsul a fost sters!");
            }

        }
        
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var prod=dataGridProduse.SelectedItem as Pizzas;
            if (prod != null)
            {
                if (dbContext.Pizzass.Count(p => p.ID == prod.ID) == 1)
                {
                    dbContext.SaveChanges();
                }
                
            }
        }

        //dupa ce fac partea de notificari
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var prod = dataGridProduse.SelectedItem as Pizzas;

            if (prod != null)
            {
               
            }
            else MessageBox.Show("Selectati un produs!");

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WindowAddProdus wp = new WindowAddProdus();
            wp.ShowDialog();
            dataGridProduse.ItemsSource = dbContext.Pizzass.ToList();

        }

        //partea de filtre
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)

        {
            string text = textBoxCautare.Text.ToLower();

            dataGridProduse.ItemsSource = dbContext.Pizzass.Where(p => p.Tip.ToString().ToLower().Contains(text)).ToList();

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    {
                        dataGridProduse.ItemsSource = dbContext.Pizzass.OrderBy(p => p.Tip.ToString()).ToList();
                        break;
                    }
                case 1:
                    {
                        dataGridProduse.ItemsSource = dbContext.Pizzass.OrderBy(p => p.Pret).ToList();
                        break;
                    }
                case 2:
                    {
                        dataGridProduse.ItemsSource = dbContext.Pizzass.OrderBy(p => p.CantitateRamasa).ToList();
                        break;
                    }
                case 3:
                    {
                        dataGridProduse.ItemsSource = dbContext.Pizzass.OrderByDescending(p => p.Tip.ToString()).ToList();
                        break;
                    }
                case 4:
                    {
                        dataGridProduse.ItemsSource = dbContext.Pizzass.OrderByDescending(p => p.Pret).ToList();
                        break;
                    }
                case 5:
                    {
                        dataGridProduse.ItemsSource = dbContext.Pizzass.OrderByDescending(p => p.CantitateRamasa).ToList();
                        break;
                    }

            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            textBoxpret1.Text = string.Empty;
            textBoxpret2.Text = string.Empty;
            comboBox.SelectedIndex = -1;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            int min, max;
            if (textBoxpret1.Text == string.Empty || textBoxpret2.Text == string.Empty) return;
            if (int.TryParse(textBoxpret1.Text, out min) == false || int.TryParse(textBoxpret2.Text, out max) == false || min > max)
            {
                MessageBox.Show("Date invalide");
                return;
            }
            else
            {
                dataGridProduse.ItemsSource = dbContext.Pizzass.Where(p => p.Pret >= min && p.Pret <= max).ToList();
            }
        }
    }
}
