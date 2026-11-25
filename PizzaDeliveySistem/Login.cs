using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using PizzaDeliveySistem.Models;

namespace PizzaDeliveySistem
{
    public class Login
    {
        private AppDataContext dbContext = new AppDataContext();
        //pentru admini
        private string path = @"C:\Users\alide\Desktop\MSOA\PizzaDeliveySistem\PizzaDeliveySistem\bin\coduri_unice.txt";
        public void generare_coduri()
        {
            StreamWriter s = new StreamWriter(path);
            
            for (int i = 0; i < 3; i++)
            {
                Guid g = Guid.CreateVersion7();
                s.WriteLine(g.ToString());
            }
            s.Close();
        }
        public int is_admin(string cod)
        {
            Login login = new Login();
            StreamReader s = new StreamReader(path);
            List<string> list = new List<string>();
            while (s.EndOfStream != true)
            {
                list.Add(s.ReadLine());
            }
            s.Close();
            foreach (string l in list)
            {
                if (cod == l)
                {
                    login.generare_coduri();
                    return 1;
                    break;
                }
            }
            return 0;
        }

        public void LogIN(User u, MainWindow wm)
        {
            if (u.Rol == rol.Admin)
            {
                Window3 w = new Window3();

                w.Show();
            }
            else if (u.Rol == rol.Client)
            {
                WindowClient w = new WindowClient();
                w.Show();
            }
            else if (u.Rol == rol.Angajat)
            {
                WindowAngajat w = new WindowAngajat();
                w.Show();
            }
            else if (u.Rol == rol.Suspendat)
            {
                MessageBox.Show("Momentan contul dumneavoastra este suspendat!");
                MainWindow mw = new MainWindow();
            }
          
        }

        public Login()
        { }
    }
}
