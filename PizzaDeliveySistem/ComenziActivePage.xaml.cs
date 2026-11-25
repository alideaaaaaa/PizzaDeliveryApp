using Microsoft.VisualBasic;
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
    /// Interaction logic for ComenziActivePage.xaml
    /// </summary>
    public partial class ComenziActivePage : Page
    {
        private readonly MainWindow w = new MainWindow();
        private readonly AppDataContext dbContext = new AppDataContext();
        int Total;
        public ComenziActivePage()
        {
            InitializeComponent();
            GenerareCos();
            dataGridComenzi.ItemsSource = dbContext.Orders.Where(o => o.IDClient == GetUser.MyUser.ID &&
            (o.Status != status.Anulata && o.Status != status.Finalizata)).ToList();
            comboBox.ItemsSource = new List<string> {"Plasata","Pregetire","In livrare"  };
        }

        private void GenerareGridComenzi()
        {
            dataGridComenzi.ItemsSource = dbContext.Orders.Where(o => o.IDClient == GetUser.MyUser.ID &&
            (o.Status != status.Anulata && o.Status != status.Finalizata)).ToList();
        }
        private void GenerareCos()
        {
            int total = 0;
            if (GetUser.MyUser.Cos.Count() != 0)
            {
                dataGridCos.ItemsSource = null;
                dataGridCos.ItemsSource = GetUser.MyUser.Cos;
                foreach (var pizza in dbContext.Pizzass)
                {
                    int suma = GetUser.MyUser.Cos.Where(p => p.TipItem == pizza.Tip).Sum(p => p.CantitateItem*pizza.Pret);
                    total = total + suma;
                }
                labelTotal.Content= total.ToString();
                Total = total;

            }
            else {dataGridCos.ItemsSource = null; labelTotal.Content=string.Empty; }
        }

        //sterge
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = (ItemCos)dataGridCos.SelectedItem;
            if (dataGridCos.SelectedItem != null)
            {
                GetUser.MyUser.Cos.Remove(GetUser.MyUser.Cos.FirstOrDefault(p => p.Id == item.Id));
                GenerareCos();
            }
        }

        //modifica
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGridCos.ItemsSource != null)
            {
                GetUser.MyUser.Cos = dataGridCos.ItemsSource as List<ItemCos>;
                GenerareCos();
            }
        }

        //order
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            if (dataGridCos.ItemsSource != null)
            {
                foreach (var pizza in dbContext.Pizzass)
                {
                    int cnt = GetUser.MyUser.Cos.Where(p => p.TipItem == pizza.Tip).Sum(p => p.CantitateItem);
                    if (cnt > pizza.CantitateRamasa)
                    {
                        //GetUser.MyUser.Cos.RemoveAll(p => p.TipItem == pizza.Tip);
                        MessageBox.Show($"Pizza {pizza.Tip} mai are doar {pizza.CantitateRamasa} buc!\n Va rugam refaceti comnda!");
                        //GenerareCos();
                        return;
                    }
               
                }
                string cos = string.Empty;
                var order = dataGridComenzi.SelectedItem as Order;
                Alerta a = new Alerta();
                a.label1.Content = $"Coamnda va fi livrata la adresa {GetUser.MyUser.Adresa}.\nDaca adresa nu este corecta va rugam\n " +
                    $"modificati-o din sectiunea cont!";
                a.ShowDialog();
                if (a.DialogResult == false) return;
                Plata p = new Plata();
                p.ShowDialog();
                if (p.DialogResult == false) return;
                foreach (var i in GetUser.MyUser.Cos)
                {
                    cos = cos + i.ToString()+"\n";
                }
                int id = 0;
                if (dbContext.Orders.Count() != 0)
                
                {
                    id = dbContext.Orders.Max(o => o.ID);
                }
                dbContext.Orders.Add(new Order
                {
                    ID = ++id,
                    Cos =cos,
                    DataComanda = DateTime.Now,
                    IDClient = GetUser.MyUser.ID,
                    Status = status.Plasata,
                    Suma = Total
                });

                foreach (var pizza in dbContext.Pizzass)
                {
                    int cnt = GetUser.MyUser.Cos.Where(p => p.TipItem == pizza.Tip).Sum(p => p.CantitateItem);
                    pizza.CantitateVanduta = pizza.CantitateVanduta + cnt;
                    pizza.CantitateRamasa = pizza.CantitateRamasa - cnt;
                }
                dbContext.SaveChanges();

                GetUser.MyUser.Cos.Clear();
                GenerareCos();
                GenerareGridComenzi();
                MessageBox.Show("Comanda a fost plasată cu succes!");
                

            }
        }

        
        ComenziActive2 c = new ComenziActive2();

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            comboBox.SelectedIndex = -1;
            c.Filtrare(dateTimePicker1, dateTimePiker2,dataGridComenzi);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            comboBox.SelectedIndex = -1;
            c.Sortare2(dataGridComenzi);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            comboBox.SelectedIndex = -1;
            var order = dataGridComenzi.SelectedItem as Order;
            Alerta a = new Alerta();
            a.label1.Content = "Sigur doriti sa anulati comanda?";
            a.ShowDialog();
            if (order != null)
            {
                order.Status = status.Anulata;
                dbContext.SaveChanges();
                GenerareGridComenzi();
                var user = dbContext.Users.First(u => u.ID == GetUser.MyUser.ID);
                user.Mesaje.Add($"Comanda nr {order.ID} a fost anulata!"+"\n--------------------------------------------");
                dbContext.SaveChanges();
                GetUser.MyUser.Mesaje.Add($"Comanda nr {order.ID} a fost anulata!" + "\n--------------------------------------------");
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGridComenzi.ItemsSource = dbContext.Orders.Where(u=>u.Status==(status)comboBox.SelectedIndex && u.IDClient==GetUser.MyUser.ID).ToList();
        }
    }
}
