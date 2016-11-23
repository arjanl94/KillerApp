using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models
{
    public class Gebruiker
    {
        public string Naam { get; set; }
        public string Gebruikersnaam { get; set; }
        public Geslacht Geslacht { get; set; }
        public string Emailadres { get; set; }
        public string Wachtwoord { get; set; }
        public Abonnement Abonnement { get; set; }
        public List<Gebruiker> Volgers { get; set; }

        public Gebruiker(string naam, string gebruikersnaam, Geslacht geslacht, string email, string wachtwoord,
            Abonnement abonnement)
        {
            this.Naam = naam;
            this.Gebruikersnaam = gebruikersnaam;
            this.Geslacht = geslacht;
            this.Emailadres = email;
            this.Wachtwoord = wachtwoord;
            this.Abonnement = abonnement;
            Volgers = new List<Gebruiker>();
        }
        public Gebruiker(string naam, string gebruikersnaam, Geslacht geslacht, string email, string wachtwoord,
    Abonnement abonnement, List<Gebruiker> volgers)
        {
            this.Naam = naam;
            this.Gebruikersnaam = gebruikersnaam;
            this.Geslacht = geslacht;
            this.Emailadres = email;
            this.Wachtwoord = wachtwoord;
            this.Abonnement = abonnement;
            Volgers = volgers;
        }
        public Gebruiker(string naam, string gebruikersnaam, Geslacht geslacht, string email, string wachtwoord)
        {
            this.Naam = naam;
            this.Gebruikersnaam = gebruikersnaam;
            this.Geslacht = geslacht;
            this.Emailadres = email;
            this.Wachtwoord = wachtwoord;
            Volgers = new List<Gebruiker>();
        }

        public void AddVolger(Gebruiker gebruiker)
        {
            Volgers.Add(gebruiker);
        }
    }
}