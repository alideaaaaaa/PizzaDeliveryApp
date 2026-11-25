using PizzaDeliveySistem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for UtilizatoriPage.xaml
    /// </summary>
    public partial class UtilizatoriPage : Page
    {
        private readonly AppDataContext dbContext=new AppDataContext();
        public void treeView()
        {
            TextBlock txt = new TextBlock();
            txt.Text = "Admini";
            txt.Foreground = Brushes.Red;
            txt.FontWeight = FontWeights.Bold;
            TreeViewItem treeViewItem = new TreeViewItem();
            treeViewItem.Header = txt;
            treeViewItem.Tag = rol.Admin;
            treeNodUser.Items.Add(treeViewItem);

            TextBlock txt1 = new TextBlock();
            txt1.Text = "Angajati";
            txt1.Foreground = Brushes.Green;
            txt1.FontWeight = FontWeights.Bold;
            TreeViewItem treeViewItem1 = new TreeViewItem();
            treeViewItem1.Tag = rol.Angajat;
            treeViewItem1.Header = txt1;
            treeNodUser.Items.Add(treeViewItem1);

            TextBlock txt2 = new TextBlock();
            txt2.Text = "Clienti";
            txt2.Foreground = Brushes.Orange;
            txt2.FontWeight = FontWeights.Bold;
            TreeViewItem treeViewItem2 = new TreeViewItem();
            treeViewItem2.Tag = rol.Client;
            treeViewItem2.Header = txt2;
            treeNodUser.Items.Add(treeViewItem2);

            TextBlock txt3 = new TextBlock();
            txt3.Text = "SUSPENDATI";
            txt3.Foreground = Brushes.Black;
            txt3.FontWeight = FontWeights.Bold;
            TreeViewItem treeViewItem3 = new TreeViewItem();
            treeViewItem3.Tag = rol.Suspendat;
            treeViewItem3.Header = txt3;
            treeNodUser.Items.Add(treeViewItem3);

            foreach (var u in dbContext.Users)
            {
                TreeViewItem nodUser= new TreeViewItem();
                nodUser.Header = u.UserName;
                nodUser.Tag = u;
                switch (u.Rol)
                {
                    case Models.rol.Admin: treeViewItem.Items.Add(nodUser); break;
                    case Models.rol.Angajat: treeViewItem1.Items.Add(nodUser); break;
                    case Models.rol.Client: treeViewItem2.Items.Add(nodUser); break;
                    case Models.rol.Suspendat: treeViewItem3.Items.Add(nodUser); break;

                }
            }
            
        }
        public UtilizatoriPage()
        {
            InitializeComponent();
            dataGridView.ItemsSource = dbContext.Users.ToList();
            treeView();
            this.Loaded += (s, e) =>
            {
                foreach (var col in dataGridView.Columns)
                {
                    if (col.Header?.ToString() == "Cos" ||
                        col.Header?.ToString() == "Mesaje"|| col.Header?.ToString()=="Data Nasterii")
                    {
                        col.Visibility = Visibility.Collapsed;
                    }
                }
            };
            comboBox.ItemsSource = new List<string> { "Crescator nume","Desrescator nume"};

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
        //tb completat
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = dataGridView.SelectedItem as User;

            if (user == null) return;
            Alerta a = new Alerta();
            a.label1.Content = "Sigur doriti sa stergeti utilizatorul?\nToate comenzile sale vor fi anulate!";
            a.ShowDialog();
            if (a.DialogResult == false) return;
            foreach (var u in dbContext.Orders.Where(u => u.IDClient == user.ID && status.Finalizata!=u.Status))
                u.Status = status.Anulata;
            if (user.Rol == rol.Client || user.Rol==rol.Suspendat)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                MessageBox.Show("Utilizatorul a fost sters!");
            }
            else if (user.Rol == rol.Admin || user.Rol == rol.Angajat)
            {
                user.Rol = rol.Suspendat;
                dbContext.SaveChanges();
                MessageBox.Show("Utilizatorul a fost suspendat!");
            }
            
            dataGridView.ItemsSource = null;
            treeNodUser.Items.Clear();
            treeView();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Modificarile au fost salvate!");
            dbContext.SaveChanges();
            treeNodUser.Items.Clear();
            treeView();
            
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
                var users = dbContext.Users.Where (u => u.Rol == (rol)treeItem.Tag).ToList();
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
