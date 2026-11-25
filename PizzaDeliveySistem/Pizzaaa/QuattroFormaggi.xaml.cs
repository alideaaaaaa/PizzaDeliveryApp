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

namespace PizzaDeliveySistem.Pizzaaa
{
    /// <summary>
    /// Interaction logic for QuattroFormaggi.xaml
    /// </summary>
    public partial class QuattroFormaggi : Page
    {
        private string text =
              "Pizza Quattro Formaggi\r\n" +
                "Ingrediente:\r\n" +
                "- aluat de pizza (făină, apă, drojdie, sare, ulei de măsline)\r\n" +
                "- mozzarella\r\n" +
                "- gorgonzola\r\n" +
                "- parmezan (sau Grana Padano)\r\n" +
                "- fontina (sau alt brânzet moale)\r\n" +
                "- smântână sau bază albă (opțional)\r\n" +
                "- ulei de măsline extravirgin\r\n" +
                "- piper (după gust)";
        private readonly AppDataContext dbContext = new AppDataContext();

        private void generareLista(int nr, ItemCollection l)
        {
            for (int i = 1; i <= nr; i++)
            {
                l.Add(i);
            }
        }
        public QuattroFormaggi()
        {

            InitializeComponent();

            var p = dbContext.Pizzass.FirstOrDefault(p => p.Tip == tip.QuattroFormaggi);
            textBoxNume.Text = "QUATTRO FORMAGGI";

            if (p != null && p.CantitateRamasa!=0)
            {
                textBoxDescriere.Text = text;
                textBoxPrice.Text = p.Pret.ToString() + " LEI";
                generareLista(p.CantitateRamasa, listCantitatea.Items);
            }

            else
            {
                TextBlock t = new TextBlock();
                t.Text = "NU ESTE PE STOC!";
                textBoxDescriere.Text = t.Text;

            }

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listCantitatea.SelectedIndex != -1)
            {
                int id = GetUser.MyUser.Cos.Count();
                GetUser.MyUser.Cos.Add(new ItemCos(++id, GetUser.MyUser.ID, tip.QuattroFormaggi, int.Parse(listCantitatea.SelectedItem.ToString())));
                MessageBox.Show("Produs adaugat cu succes!");
            }
        }
    }
}

