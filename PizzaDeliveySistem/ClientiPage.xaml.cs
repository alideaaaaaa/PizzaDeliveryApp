using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for ClientiPage.xaml
    /// </summary>
    public partial class ClientiPage : Page
    {
        private readonly AppDataContext dbContext = new AppDataContext();
        public void treeView()
        {

            TextBlock txt2 = new TextBlock();
            txt2.Text = "Clienti";
            txt2.Foreground = Brushes.Orange;
            txt2.FontWeight = FontWeights.Bold;
            TreeViewItem treeViewItem2 = new TreeViewItem();
            treeViewItem2.Tag = rol.Client;
            treeViewItem2.Header = txt2;
            treeNodUser.Items.Add(treeViewItem2);

          
            foreach (var u in dbContext.Users.Where(u=>u.Rol==rol.Client))
            {
                TreeViewItem nodUser = new TreeViewItem();
                nodUser.Header = u.UserName;
                nodUser.Tag = u;
                
                treeViewItem2.Items.Add(u);
                
            }

        }
        public ClientiPage()
        {
            InitializeComponent();
            dataGridView.ItemsSource = dbContext.Users.Where(u=>u.Rol==rol.Client).ToList();
            treeView();
            this.Loaded += (s, e) =>
            {
                foreach (var col in dataGridView.Columns)
                {
                    if (col.Header?.ToString() == "Cos" ||
                        col.Header?.ToString() == "Mesaje" || col.Header?.ToString() == "Data Nasterii")
                    {
                        col.Visibility = Visibility.Collapsed;
                    }
                }
            };
            comboBox.ItemsSource = new List<string> { "Crescator nume", "Desrescator nume" };

        }
        private void treeNodUser_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selected = treeNodUser.SelectedItem as TreeViewItem;
            if (selected == null) return;
            if (selected.Tag is rol r)
            {
                dataGridView.ItemsSource = dbContext.Users
                    .Where(u => u.Rol == r)
                    .ToList();
            }
            else if (selected.Tag is User user)
            {
                dataGridView.ItemsSource = new List<User> { user };
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var treeItem = treeNodUser.SelectedItem as TreeViewItem;
            if (treeItem == null) return;
            WindowMesaj window = new WindowMesaj();
            window.ShowDialog();
            if (window.DialogResult == true && treeItem.Tag is User)
            {
                var user = (User)treeItem.Tag;
                MessageBox.Show($"Mesajul a fost trimis catre [{user.UserName}]!");
                user.Mesaje.Add(window.textBoxMesaj.Text + "\n---------------------------------------------");

            }
            else if (window.DialogResult == true && treeItem.Tag is rol)
            {
                var users = dbContext.Users.Where(u => u.Rol == (rol)treeItem.Tag).ToList();
                foreach (var u in users)
                {
                    u.Mesaje.Add(window.textBoxMesaj.Text + "\n---------------------------------------------");
                }
                dbContext.SaveChanges();
            }

        }

        private void treeNodUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGridView.ItemsSource = dbContext.Users.ToList();
            comboBox.SelectedIndex = -1;

        }



        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedIndex == 0)
            {
                dataGridView.ItemsSource = dbContext.Users.OrderBy(u => u.Nume).ToList();
            }
            else if (comboBox.SelectedIndex == 1)
            {
                dataGridView.ItemsSource = dbContext.Users.OrderByDescending(u => u.Nume).ToList();
            }

        }

        private void textBoxCautare_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = textBoxCautare.Text.ToLower();

            dataGridView.ItemsSource = dbContext.Users.Where(p => p.Nume.Contains(text)).ToList();

        }
    }
}

