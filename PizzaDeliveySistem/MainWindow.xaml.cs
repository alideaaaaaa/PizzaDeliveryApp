using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.RightsManagement;
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
using Microsoft.EntityFrameworkCore;
using PizzaDeliveySistem.Models;

namespace PizzaDeliveySistem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int cnt;
        private Login ilogin = new Login();
        private AppDataContext dbContext = new AppDataContext();
        public MainWindow()
        {
           InitializeComponent();
            dbContext.Database.EnsureCreated();
        }

        //creare cont
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
             w.ShowDialog();
             if (w.DialogResult == true)
             {
                 int id = dbContext.Users.Max(u=>u.ID);
                 dbContext.Users.Add(new User { ID= ++id,
                     UserName = w.textBoxUser.Text,
                     Pass = w.textBoxPass.Text,
                     Nume = w.textBoxNume.Text,
                     DataNasterii = w.timePickerData.SelectedDate.Value,
                     Adresa = w.textBoxAdresa.Text,
                     Email = w.textBoxEmail.Text,
                     Rol =rol.Client});
                 dbContext.SaveChanges();
             }
        }

        //autentif
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            int ok = 0;
            var user = dbContext.Users.FirstOrDefault(u => u.UserName == textBoxUsername.Text
                      && u.Pass == textBoxPass.Text);
           
            if (user !=null)
            {
                GetUser.MyUser = user;

                ok = 1;
                ilogin.LogIN(user,this);
                this.Close();
                   
            }
            
            if (ok == 0)
            {
               
                MessageBox.Show("Username sau parola gresite!");
            }
        }
        
    }
}