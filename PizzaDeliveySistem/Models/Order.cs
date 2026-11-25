using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDeliveySistem.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Cos { get; set; }
        public int IDClient { get; set; }
        public DateTime DataComanda { get; set; }
        public status Status { get; set; }
        public int Suma { get; set; }
    }
    public enum status
    {
        Plasata,
        Pregatire,
        livrare,
        Finalizata,
        Anulata
    }
}
