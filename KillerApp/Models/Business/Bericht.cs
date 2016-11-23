using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models
{
    public class Bericht
    {
        public Gebruiker Verzender { get; set; }
        public Gebruiker Ontvanger { get; set; }
        public string Titel { get; set; }
        public string Tekst { get; set; }

        public Bericht(Gebruiker verzender, Gebruiker ontvanger, string titel, string tekst)
        {
            this.Verzender = verzender;
            this.Ontvanger = ontvanger;
            this.Titel = titel;
            this.Tekst = tekst;
        }
    }
}