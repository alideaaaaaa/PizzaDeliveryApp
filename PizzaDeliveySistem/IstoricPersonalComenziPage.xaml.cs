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
    /// Interaction logic for IstoricPersonalComenziPage.xaml
    /// </summary>
    public partial class IstoricPersonalComenziPage : Page
    {
        private readonly ComenziActive2 c = new ComenziActive2();
        private readonly AppDataContext dbContext = new AppDataContext();
        public IstoricPersonalComenziPage()
        {
            InitializeComponent();
            dataGrid.ItemsSource = dbContext.Orders.Where(o => o.Status == Models.status.Finalizata && o.IDClient==GetUser.MyUser.ID).ToList();
            comboBox.ItemsSource=new List<string> { "Crescator dupa suma","Crescator dupa data","Descrescator dupa data","Descrescator dupa suma"};
        }
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            c.Filtrare2(dateTime1, dateTime2, dataGrid);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dateTime2.SelectedDate = null;
            dateTime1.SelectedDate = null;
            comboBox.SelectedIndex = -1;
            dataGrid.ItemsSource = dbContext.Orders.Where(p => p.IDClient == GetUser.MyUser.ID && p.Status == Models.status.Finalizata).ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    {
                        dataGrid.ItemsSource = dbContext.Orders.Where(p => p.IDClient == GetUser.MyUser.ID && p.Status == Models.status.Finalizata).OrderBy(o=>o.Suma).ToList();
                        break; 
                    }
                case 1:
                    {
                        dataGrid.ItemsSource = dbContext.Orders.Where(p => p.IDClient == GetUser.MyUser.ID && p.Status == Models.status.Finalizata).OrderBy(o => o.DataComanda).ToList();
                        break;
                    }
                case 2:
                    {
                        dataGrid.ItemsSource = dbContext.Orders.Where(p => p.IDClient == GetUser.MyUser.ID && p.Status == Models.status.Finalizata).OrderByDescending(o => o.DataComanda).ToList();
                        break;
                    }
                case 3:
                    {
                        dataGrid.ItemsSource = dbContext.Orders.Where(p => p.IDClient == GetUser.MyUser.ID && p.Status == Models.status.Finalizata).OrderByDescending(o => o.Suma).ToList();
                        break;
                    }
            }
        }
    }
}
