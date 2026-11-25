using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PizzaDeliveySistem.Models;

namespace PizzaDeliveySistem
{
    class AppDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pizzas> Pizzass { get; set; }
        public DbSet<Order> Orders { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=PizzDelivery.db");
        }
    }
}
