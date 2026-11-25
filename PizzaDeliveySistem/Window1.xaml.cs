using Microsoft.EntityFrameworkCore;
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
using PizzaDeliveySistem.Models;

namespace PizzaDeliveySistem
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        int id = 100;
        private AppDataContext dbContext = new AppDataContext();
        private Login login = new Login();
        public Window1()
        {
            InitializeComponent();
            dbContext.Database.EnsureCreated();
        }
        public int Verificari()
        {
           
            if (string.IsNullOrWhiteSpace(textBoxNume.Text))
            {
                textBoxNume.BorderBrush = Brushes.Red;
                MessageBox.Show("Campul Nume trebuie completat!");
                return 0;
            }
            else if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                textBoxEmail.BorderBrush = Brushes.Red;
                MessageBox.Show("Campul Email trebuie completat!");
                return 0;
            }
            else if (timePickerData.SelectedDate >= DateTime.Now || timePickerData.SelectedDate == null)
            {
                timePickerData.BorderBrush = Brushes.Red;
                MessageBox.Show("Campul Data trebuie completat corect!");

                return 0;
            }
            else if (string.IsNullOrWhiteSpace(textBoxUser.Text))
            {
                textBoxUser.BorderBrush = Brushes.Red;
                MessageBox.Show("Campul User trebuie completat!");
                return 0;
            }
            else if (string.IsNullOrWhiteSpace(textBoxPass.Text))
            {
                textBoxPass.BorderBrush = Brushes.Red;
                MessageBox.Show("Campul Parola trebuie completat!");
                return 0;
            }
            else if (string.IsNullOrWhiteSpace(textBoxAdresa.Text))
            {
                textBoxAdresa.BorderBrush = Brushes.Red;
                MessageBox.Show("Campul Adresa trebuie completat!");

                return 0;
            }
           
           
            return 1;
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Verificari() == 0)   return;
            if (login.is_admin(textBoxPass.Text) == 1)
            {
                Window2 w = new Window2();
                w.ShowDialog();
                int id = dbContext.Users.Max(u=>u.ID);
                dbContext.Users.Add(new User
                {
                    ID = ++id,
                    UserName = textBoxUser.Text,
                    Pass = w.textBoxpassAdmin.Text,
                    Nume = textBoxNume.Text,
                    DataNasterii = timePickerData.SelectedDate.Value,
                    Adresa = textBoxAdresa.Text,
                    Email = textBoxEmail.Text,
                    Rol = 0
                });
                dbContext.SaveChanges();
                this.DialogResult = false;
                this.Close();
            }
            else
            { 
                this.DialogResult = true; this.Close(); }
        }
    }
}
