using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzaDeliveySistem.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        
        public string Pass { get; set; }

        public string Nume { get; set; }
        public DateTime DataNasterii { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public rol Rol { get; set; }

        
        public List<ItemCos> Cos { get; set; } = new List<ItemCos>();
        public List<string> Mesaje { get; set; }= new List<string>();


        
    }
    public enum rol
    {
        Admin,
        Angajat,
        Client,
        Suspendat


    }
}
