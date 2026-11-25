using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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
    /// Interaction logic for PageStatistici.xaml
    /// </summary>
    public partial class PageStatistici : Page
    {
        private readonly AppDataContext dbContext=new AppDataContext();
        public PageStatistici()
        {
            InitializeComponent();
            labelctive.Content = dbContext.Orders.Count(o => (o.Status != status.Finalizata) || (o.Status != status.Anulata));
            labelfinaliate.Content = dbContext.Orders.Count(o=>o.Status==status.Finalizata);
            labelTotal.Content = dbContext.Orders.Sum(o => o.Suma);
            var pizza = dbContext.Pizzass.ToList();

            chart.Series = new ISeries[]
            {
                new LineSeries<int>
                {
                    Values = pizza.Select(p => p.CantitateVanduta).ToArray()
                }
            };

            chart.XAxes = new[]
            {
                new Axis
                {
                    Labels = pizza.Select(p => p.Tip.ToString()).ToArray()
                }
            };
        }
    }
}
