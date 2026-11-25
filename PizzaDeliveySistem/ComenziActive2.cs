using PizzaDeliveySistem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PizzaDeliveySistem
{
    internal class ComenziActive2
    {
        private readonly AppDataContext dbContext = new AppDataContext();

        public void Filtrare(DatePicker dt, DatePicker dt2, DataGrid grid)
        {
            grid.ItemsSource = null;
            if (dt.SelectedDate == null || dt2.SelectedDate == null)
            {
                MessageBox.Show("Pentru a cauta dupa data\ncompletati ambele campuri!");
            }
            else if (dt.SelectedDate != null || dt2.SelectedDate != null)
            {
                grid.ItemsSource = dbContext.Orders.Where(o => o.DataComanda >= dt.SelectedDate && o.DataComanda <= dt2.SelectedDate && GetUser.MyUser.ID == o.IDClient && o.Status != status.Finalizata
                && o.Status != status.Anulata ).ToList();
            }
            else grid.ItemsSource = dbContext.Orders.ToList();
        }

        public void Filtrare2(DatePicker dt, DatePicker dt2, DataGrid grid)
        {
            grid.ItemsSource = null;
            if (dt.SelectedDate == null || dt2.SelectedDate == null)
            {
                MessageBox.Show("Pentru a cauta dupa data\ncompletati ambele campuri!");
            }
            else if (dt.SelectedDate != null || dt2.SelectedDate != null)
            {
                grid.ItemsSource = dbContext.Orders.Where(o => o.DataComanda >= dt.SelectedDate && GetUser.MyUser.ID==o.IDClient && o.DataComanda <= dt2.SelectedDate && o.Status == status.Finalizata
               ).ToList();
            }
            else grid.ItemsSource = dbContext.Orders.ToList();
        }

        public void Filtrare3(DatePicker dt, DatePicker dt2, DataGrid grid)
        {
            grid.ItemsSource = null;
            if (dt.SelectedDate == null || dt2.SelectedDate == null)
            {
                MessageBox.Show("Pentru a cauta dupa data\ncompletati ambele campuri!");
            }
            else if (dt.SelectedDate != null || dt2.SelectedDate != null)
            {
                grid.ItemsSource = dbContext.Orders.Where(o => o.DataComanda >= dt.SelectedDate && o.DataComanda <= dt2.SelectedDate).ToList();
            }
            else grid.ItemsSource = dbContext.Orders.ToList();
        }

        public void Sortare(DataGrid grid)
        {

            if (grid.ItemsSource != null)
            {
                grid.ItemsSource = (grid.ItemsSource as List<Order>).OrderByDescending(o => o.DataComanda).ToList();

            }

        }
        public void Sortare2(DataGrid grid)
        {

            if (grid.ItemsSource != null)
            {
                grid.ItemsSource =dbContext.Orders.Where(u=>u.IDClient==GetUser.MyUser.ID && u.Status!=status.Finalizata && u.Status!=status.Anulata).OrderByDescending(o => o.DataComanda).ToList();

            }

        }
    }
}
    
