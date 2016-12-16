using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models
{
    public class Muziek : Content
    {
        public int kHz { get; set; }

        public Muziek(int nr, string naam, string beschrijving, TimeSpan duur, Genre genre, Gebruiker uploader, int kHz) : base(nr, naam, beschrijving, duur, genre, uploader)
        {
            this.Nr = nr;
            this.Naam = naam;
            this.Beschrijving = beschrijving;
            this.Duur = duur;
            this.Genre = genre;
            this.Uploader = uploader;
            this.kHz = kHz;
        }
    }
}