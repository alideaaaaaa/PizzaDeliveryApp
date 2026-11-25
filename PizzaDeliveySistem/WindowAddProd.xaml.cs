using PizzaDeliveySistem.Models;
using PizzaDeliveySistem.Models;
using PizzaDeliveySistem.Pizzaaa;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowAddProdus : Window
    {
        private readonly AppDataContext dbContext = new AppDataContext();
        public WindowAddProdus()
        {
            InitializeComponent();
            List<string> pizzas =
                [ "Margerita",
                "ProsciuttoCrudo",
                "Napoletana",
                "Capriciosa",
                "Peperoni",
                "QuattroFormaggi",
                "MortadellaBurata",
                "Pesto"];
            comboBox.ItemsSource = pizzas ;
            int i = 1;
         
            while (i <= 70)
            {
                comboBox2.Items.Add(++i);
            }

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id = dbContext.Pizzass.Max(u=>u.ID);
            int price;
            if (dbContext.Pizzass.FirstOrDefault(p => p.Tip == (tip)comboBox.SelectedIndex) != null)
            {
                MessageBox.Show("Produsul deja exista!\nVa rugam modificati-l in tabel!");
                this.Close();
                return;
            }
            if (int.TryParse(textBox.Text, out price) == true && comboBox.SelectedIndex != -1
                && comboBox2.SelectedIndex != -1)
            {
                dbContext.Pizzass.Add(new Pizzas
                {
                    ID = ++id,
                    CantitateRamasa = (int)comboBox2.SelectedItem,
                    CantitateVanduta = 0,
                    Pret = price,
                    Tip = (tip)comboBox.SelectedIndex
                });
                dbContext.SaveChanges();

            }
            else
            {
                MessageBox.Show("Date invalide!");
                return;
            }
            MessageBox.Show("Produs adaugat cu succes!");
            this.Close();
        }
    }
}
