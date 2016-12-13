using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models
{
    public class Abonnement
    {
        public string Naam { get; set; }
        public double Prijs { get; set; }
        public string Beschrijving { get; set; }

        public Abonnement(string naam, double prijs, string beschrijving)
        {
            this.Naam = naam;
            this.Prijs = prijs;
            this.Beschrijving = beschrijving;
        }

        public override string ToString()
        {
            return $"{Naam} {Prijs} {Beschrijving}";
        }
    }
}