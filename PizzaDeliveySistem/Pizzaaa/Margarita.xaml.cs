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
using PizzaDeliveySistem.Models;
using Microsoft.EntityFrameworkCore;

namespace PizzaDeliveySistem.Pizza
{
    /// <summary>
    /// Interaction logic for Margarita.xaml
    /// </summary>
    public partial class MargaritaPage : Page
    {

        private string text =
            "Pizza Margherita\r\n" +
            "Ingrediente:\r\n" +
            "- aluat de pizza (făină, apă, drojdie, sare, ulei de măsline)\r\n" +
            "- sos de roșii\r\n" +
            "- mozzarella\r\n" +
            "- busuioc proaspăt\r\n" +
            "- ulei de măsline (opțional)\r\n" +
            "- sare (după gust)";
        private readonly AppDataContext dbContext = new AppDataContext();
        
        private void generareLista(int nr, ItemCollection l)
        {
            for (int i = 1; i <= nr; i++)
            {
                l.Add(i);
            }
        }
        public MargaritaPage()
        {
           
            InitializeComponent();
            
            var p = dbContext.Pizzass.FirstOrDefault(p => p.Tip == tip.Margerita);
            textBoxNume.Text = "MARGERITA";


            if(p==null || p.CantitateRamasa==0)
            { 
                TextBlock t= new TextBlock();
                t.Text = "NU ESTE PE STOC!";
                textBoxDescriere.Text = t.Text;
            
            }
            else
            {
                textBoxDescriere.Text = text;
                textBoxPrice.Text = p.Pret.ToString() + " LEI";
                generareLista(p.CantitateRamasa, listCantitatea.Items);
            }

        }

      
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listCantitatea.SelectedIndex != -1)
            {
                int id = GetUser.MyUser.Cos.Count();
                GetUser.MyUser.Cos.Add(new ItemCos(++id, GetUser.MyUser.ID, tip.Margerita, int.Parse(listCantitatea.SelectedItem.ToString())));
                
                MessageBox.Show("Produs adaugat cu succes!");
            }
        }
    }
}
