using PizzaDeliveySistem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PizzaDeliveySistem
{
    public class ItemCos
    {
        public int Id { get; set; }
        [Browsable(false)] public int IDclient { get; set; }
        public tip TipItem { get; set; }
        public int CantitateItem { get; set; }

        public ItemCos() { }
        public ItemCos(int id,int idclient,tip tipItem, int cantitateItem )
        {
            Id = id;
            IDclient = idclient;
            TipItem = tipItem;
            CantitateItem= cantitateItem;
            
        }

        public override string ToString()
        {
            return $"{Id} {TipItem} x\n  {CantitateItem} buc.";
        }

        
    }
}
