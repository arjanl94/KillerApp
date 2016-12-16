using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor;

namespace KillerApp.Models
{
    public abstract class Content
    {
        public int Nr { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public TimeSpan Duur { get; set; }
        public Genre Genre { get; set; }
        public Gebruiker Uploader { get; set; }
        public List<Reactie> Reacties { get; set; }

        public Content(int nr, string naam, string beschrijving, TimeSpan duur, Genre genre, Gebruiker uploader)
        {
            Nr = nr;
            Naam = naam;
            Beschrijving = beschrijving;
            Duur = duur;
            Genre = genre;
            Uploader = uploader;
        }
    }
}