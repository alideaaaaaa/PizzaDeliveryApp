using PizzaDeliveySistem.Pizza;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDeliveySistem.Models
{
    public class Pizzas
    {
        public int ID { get; set; }
        public int Pret{ get; set; }
        public tip Tip { get; set; }
        public int CantitateRamasa { get; set; }
        public int CantitateVanduta { get; set; } //mereu 0
       

        public override string ToString()
        {
            return $"{ID} de tipul {Tip.ToString()}  cu {CantitateRamasa} buc ramase";
        }
    }
    public enum tip
    {
        Margerita,
        ProsciuttoCrudo,
        Napoletana,
        Capriciosa,
        Peperoni,
        QuattroFormaggi,
        MortadellaBurata,
        Pesto
    }
   
}
