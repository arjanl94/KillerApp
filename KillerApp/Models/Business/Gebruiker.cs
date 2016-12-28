using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DataType(DataType.EmailAddress)]
        public string Emailadres { get; set; }

        [DataType(DataType.Password)]
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

        public Gebruiker(int gebruikernr, string naam, string gebruikersnaam, string email, string wachtwoord)
        {
            this.Gebruikernr = gebruikernr;
            this.Naam = naam;
            this.Gebruikersnaam = gebruikersnaam;
            this.Emailadres = email;
            this.Wachtwoord = wachtwoord;
        }
        public Gebruiker(string naam, string gebruikersnaam, string geslacht, string email, string wachtwoord)
        {
            this.Naam = naam;
            this.Gebruikersnaam = gebruikersnaam;
            this.Emailadres = email;
            this.Wachtwoord = wachtwoord;
            this.Geslacht = (Geslacht)Enum.Parse(typeof(Geslacht), geslacht);
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