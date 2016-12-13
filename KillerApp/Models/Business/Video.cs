using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models
{
    public class Video : Content
    {
        public Resolutie Resolutie { get; set; }
        public Video(string naam, string beschrijving, TimeSpan duur, Genre genre, Gebruiker uploader, string resolutie) : base(naam, beschrijving, duur, genre, uploader)
        {
            this.Naam = naam;
            this.Beschrijving = beschrijving;
            this.Duur = duur;
            this.Genre = genre;
            this.Uploader = uploader;
            this.Resolutie = Resolutie.LowResolution;
        }
    }
}