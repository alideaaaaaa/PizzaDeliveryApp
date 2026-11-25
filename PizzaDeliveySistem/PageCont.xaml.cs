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
    /// Interaction logic for PageCont.xaml
    /// </summary>
    public partial class PageCont : Page
    {
        AppDataContext dbContext = new AppDataContext();

        public PageCont()
        {
            InitializeComponent();
            Generare();
            
        }

        public void Generare()
        {
            var user = dbContext.Users.First(u => u.ID == GetUser.MyUser.ID);
            labelNume.Content = user.Nume;
            labelUser.Content = user.UserName;
            labelData.Content = user.DataNasterii.ToString();
            textBoxAdresa.Text = user.Adresa;
            textBoxEmail.Text = user.Email;
            if (user.Mesaje.Count != 0)
                listBox.ItemsSource = user.Mesaje;
            else listBox.ItemsSource =  new List<string> { "NU EXISTA MESAJE NOI!" };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var u = dbContext.Users.First(u => u.ID == GetUser.MyUser.ID) as User;
            if (u != null)
            {
                u.Email = textBoxEmail.Text;
                u.Adresa = textBoxAdresa.Text;
                dbContext.SaveChanges();
                textBoxAdresa.Text = u.Adresa;
                textBoxEmail.Text = u.Email;
            }
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var u = dbContext.Users.First(u => u.ID == GetUser.MyUser.ID) as User;
            if (u != null)
            {
                u.Mesaje.Clear();
                dbContext.SaveChanges();
                GetUser.MyUser.Mesaje.Clear();
                listBox.ItemsSource = null;
                listBox.ItemsSource = new List<string> { "NU EXISTA MESAJE NOI!" };

            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GetUser.MyUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            Window.GetWindow(this).Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = textBoxCautare.Text.ToLower();
            var u = dbContext.Users.First(u => u.ID == GetUser.MyUser.ID) as User;

            listBox.ItemsSource = u.Mesaje.Where(m => m.ToLower().Contains(text)).ToList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Alerta a = new Alerta();
            a.label1.Content = "Sigur doriti sa va stergeti contul?";
            a.ShowDialog();
            if (a.DialogResult == true)
            {
                var user = dbContext.Users.First(u => u.ID == GetUser.MyUser.ID);
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }

          
        }
    }
}
