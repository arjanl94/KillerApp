using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerApp.Models.Data_Access;

namespace KillerApp.Models
{
    public class Gebruiker
    {
        public int Gebruikernr { get; set; }
        public string Naam { get; set; }
        public string Gebruikersnaam { get; set; }
        public Geslacht Geslacht { get; set; }
        public string Emailadres { get; set; }
        public string Wachtwoord { get; set; }
        public Abonnement Abonnement { get; set; }
        public List<Gebruiker> Volgers { get; set; }
        public bool Admin { get; set; }

        public Gebruiker(int gebruikernr, string abonnement, string naam, string gebruikersnaam, Geslacht geslacht, string email, string wachtwoord)
        {
            this.Gebruikernr = gebruikernr;
            if (abonnement != "null")
            {
                AbonnementRepository listAbonnementRepository = new AbonnementRepository(new MssqlAbonnementLogic());
                List<Abonnement> abonnementen = listAbonnementRepository.ListAbonnementen();
                this.Abonnement = abonnementen.Single(Abonnement => Abonnement.Naam == abonnement);
            }
            else
            {
                this.Abonnement = new Abonnement("Geen", 0, "Geen abonnement");
            }
            this.Naam = naam;
            this.Gebruikersnaam = gebruikersnaam;
            this.Geslacht = geslacht;
            this.Emailadres = email;
            this.Wachtwoord = wachtwoord;
            Volgers = new List<Gebruiker>();
            Admin = false;
            if (naam == "Admin")
            {
                Admin = true;
            }
        }
        public Gebruiker(string abonnement, string naam, string gebruikersnaam, Geslacht geslacht, string email, string wachtwoord)
        {
            if (abonnement != "null")
            {
                AbonnementRepository listAbonnementRepository = new AbonnementRepository(new MssqlAbonnementLogic());
                List<Abonnement> abonnementen = listAbonnementRepository.ListAbonnementen();
                this.Abonnement = abonnementen.Single(Abonnement => Abonnement.Naam == abonnement);
            }
            else
            {
                this.Abonnement = new Abonnement("Geen", 0, "Geen abonnement");
            }
            this.Naam = naam;
            this.Gebruikersnaam = gebruikersnaam;
            this.Geslacht = geslacht;
            this.Emailadres = email;
            this.Wachtwoord = wachtwoord;
            Volgers = new List<Gebruiker>();
            Admin = false;
            if (naam == "Admin")
            {
                Admin = true;
            }
        }

        public void AddVolger(Gebruiker gebruiker)
        {
            Volgers.Add(gebruiker);
        }

        public override string ToString()
        {
            return @"Naam: " + Naam;
        }
    }
}