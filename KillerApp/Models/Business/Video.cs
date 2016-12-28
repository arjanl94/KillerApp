using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models
{
    public class Video : Content
    {
        public Resolutie Resolutie { get; set; }
        public Video(int nr, string naam, string beschrijving, TimeSpan duur, Genre genre, Gebruiker uploader, string resolutie, int contentnr) : base(nr, naam, beschrijving, duur, genre, uploader, contentnr)
        {
            this.Nr = nr;
            this.Naam = naam;
            this.Beschrijving = beschrijving;
            this.Duur = duur;
            this.Genre = genre;
            this.Uploader = uploader;
            if (resolutie =="1080P")
            {
                this.Resolutie = Resolutie._1080P;
            }
            if (resolutie == "720P")
            {
                this.Resolutie = Resolutie._720P;
            }
            if (resolutie == "480P")
            {
                this.Resolutie = Resolutie._480P;
            }
            if (resolutie == "Low Resolution")
            {
                this.Resolutie = Resolutie.LowResolution;
            }
            this.Video = true;
            this.Contentnr = contentnr;
        }

        public Video(string naam, string beschrijving, TimeSpan duur, Genre genre, Gebruiker uploader, string resolutie) : base(naam, beschrijving, duur, genre, uploader)
        {
            this.Naam = naam;
            this.Beschrijving = beschrijving;
            this.Duur = duur;
            this.Genre = genre;
            this.Uploader = uploader;
            if (resolutie == "1080P")
            {
                this.Resolutie = Resolutie._1080P;
            }
            if (resolutie == "720P")
            {
                this.Resolutie = Resolutie._720P;
            }
            if (resolutie == "480P")
            {
                this.Resolutie = Resolutie._480P;
            }
            if (resolutie == "Low Resolution")
            {
                this.Resolutie = Resolutie.LowResolution;
            }
            this.Video = true;
        }
    }
}