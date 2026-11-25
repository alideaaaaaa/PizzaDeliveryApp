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
    /// Interaction logic for PrCrudo.xaml
    /// </summary>
    public partial class PrCrudo : Page
    {
        private string text =
                   "Pizza Crudo e Prosciutto\r\n" +
            "Ingrediente:\r\n" +
            "- aluat de pizza (făină, apă, drojdie, sare, ulei de măsline)\r\n" +
            "- sos de roșii (sau bază albă, după preferință)\r\n" +
            "- mozzarella\r\n" +
            "- prosciutto crudo (șuncă crud-uscată)\r\n" +
            "- rucola proaspătă\r\n" +
            "- parmezan ras\r\n" +
            "- ulei de măsline extravirgin\r\n" +
            "- sare și piper (după gust)";

        private readonly AppDataContext dbContext = new AppDataContext();

        private void generareLista(int nr, ItemCollection l)
        {
            for (int i = 1; i <= nr; i++)
            {
                l.Add(i);
            }
        }
        public PrCrudo()
        {

            InitializeComponent();

            var p = dbContext.Pizzass.FirstOrDefault(p => p.Tip == tip.ProsciuttoCrudo);
            textBoxNume.Text = "PROSCIUTTO CRUDO";

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
                GetUser.MyUser.Cos.Add(new ItemCos(++id, GetUser.MyUser.ID, tip.ProsciuttoCrudo, int.Parse(listCantitatea.SelectedItem.ToString())));
                MessageBox.Show("Produs adaugat cu succes!");
            }
        }
    }
}

