using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models
{
    public class Reactie
    {
        public int Reactienr { get; set; }
        public int Contentnr { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public string Tekst { get; set; }

        public Reactie(int reactienr, Gebruiker gebruiker, string tekst)
        {
            this.Reactienr = reactienr;
            this.Gebruiker = gebruiker;
            this.Tekst = tekst;
        }
        public Reactie(Gebruiker gebruiker, string tekst, int contentnr)
        {
            this.Gebruiker = gebruiker;
            this.Tekst = tekst;
            this.Contentnr = contentnr;
        }
    }
}