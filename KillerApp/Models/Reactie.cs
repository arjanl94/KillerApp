using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models
{
    public class Reactie
    {
        public Gebruiker Gebruiker { get; set; }
        public string Tekst { get; set; }
        public Content Content { get; set; }

        public Reactie(Gebruiker gebruiker, string tekst, Content content)
        {
            this.Gebruiker = gebruiker;
            this.Tekst = tekst;
            this.Content = content;
        }
    }
}