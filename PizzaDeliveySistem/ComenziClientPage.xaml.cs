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
    /// Interaction logic for ComenziClientPage.xaml
    /// </summary>
    public partial class ComenziClientPage : Page
    {
        public ComenziClientPage()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComenziFrame.Navigate(new ComenziActivePage());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ComenziFrame.Navigate(new IstoricPersonalComenziPage());
        }

        private void ComenziFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
