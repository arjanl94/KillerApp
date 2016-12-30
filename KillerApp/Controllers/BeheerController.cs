using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerApp.Models;
using KillerApp.Models.Data_Access;

namespace KillerApp.Controllers
{
    public class BeheerController : Controller
    {
        private GebruikerRepository gebruikerRepository = new GebruikerRepository(new MssqlGebruikerLogic());
        private AbonnementRepository abonnementRepository = new AbonnementRepository(new MssqlAbonnementLogic());
        private ScheldwoordRepository scheldwoordRepository = new ScheldwoordRepository(new MssqlScheldwoordLogic());
        private BerichtRepository berichtRepository = new BerichtRepository(new MssqlBerichtLogic());
        private int ongepastaantal = 2;
        // GET: Beheer
        
        public ActionResult Gebruikers()
        {
            //Kijkt of er een gebruiker is ingelogd
            if (Session["Gebruiker"] != null)
            {
                Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
                //Kijkt of de gebruiker adminrechten heeft. Als dit zo is wordt de lijst met gebruikers getoond.
                if (gebruiker.Admin == true)
                {
                    List<Gebruiker> gebruikers = gebruikerRepository.ListGebruikers();
                    return View(gebruikers);
                }
                //Als de gebruiker geen admin is wordt de pagina van content getoond.
                else
                {
                    return RedirectToAction("All", "Content");
                }
            }
            else
            {
                return RedirectToAction("All", "Content");
            }
        }

        [HttpGet]
        public ActionResult AddGebruiker()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGebruiker(FormCollection form)
        {
            //Haalt alle gegevens uit de Form en wordt vervolgens gebruikt om een gebruiker aan te maken.
            string naam = form["Naam"];
            string gebruikersnaam = form["Gebruikersnaam"];
            string email = form["Emailadres"];
            string geslacht = form["Geslacht"];
            string wachtwoord = form["Wachtwoord"];
            gebruikerRepository.AddGebruiker(new Gebruiker(naam, gebruikersnaam, geslacht, email, wachtwoord));
            return RedirectToAction("Gebruikers");
        }

        [HttpGet]
        public ActionResult WijzigGebruiker(string email)
        {
            //Haalt een lijst gebruikers op en zoekt vervolgens de juiste gebruiker aan de hand van de email dat is meegegeven aan de Action
            List<Gebruiker> gebruikers = gebruikerRepository.ListGebruikers();
            Gebruiker user = gebruikers.Find(gebruiker => gebruiker.Emailadres == email);
            return View(user);
        }

        [HttpPost]
        public ActionResult WijzigGebruiker(FormCollection form, string email)
        {
            //De email wordt meegegeven met de Action, de overige informatie wordt uit de form gehaald om een gebruiker te wijzigen.
            //Dit is gedaan omdat de email niet kan worden gewijzigd.
            int gebruikernr = Convert.ToInt32(form["Gebruikernr"]);
            string naam = form["Naam"];
            string gebruikersnaam = form["Gebruikersnaam"];
            string wachtwoord = form["Wachtwoord"];
            Gebruiker gebruiker = new Gebruiker(gebruikernr, naam, gebruikersnaam, email, wachtwoord);
            gebruikerRepository.EditGebruiker(gebruiker);
            return RedirectToAction("Gebruikers");
        }

        public ActionResult VerwijderGebruiker(int Gebruikernr)
        {
            //Aan de hand van de gebruikernr wordt de juiste gebruiker verwijdert.
            gebruikerRepository.RemoveGebruiker(Gebruikernr);
            return RedirectToAction("Gebruikers");
        }

        public ActionResult Abonnementen()
        {
            if (Session["Gebruiker"] != null)
            {
                Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
                //Alleen als de gebruiker een admin is wordt de lijst met berichten getoond
                if (gebruiker.Admin == true)
                {
                    List<Abonnement> abonnementen = abonnementRepository.ListAbonnementen();
                    return View(abonnementen);
                }
                else
                {
                    return RedirectToAction("All", "Content");
                }
            }
            else
            {
                return RedirectToAction("All", "Content");
            }
        }

        [HttpGet]
        public ActionResult AddAbonnement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbonnement(FormCollection form)
        {
            string naam = form["Naam"];
            double prijs = Convert.ToDouble(form["Prijs"]);
            string beschrijving = form["Beschrijving"];

            Abonnement abo = new Abonnement(naam, prijs, beschrijving);
            try
            {
                abonnementRepository.AddAbonnement(abo);
            }
            catch
            {
                ModelState.AddModelError("Gebruik", "Naam al in gebruik");
                return View();
            }
            return RedirectToAction("Abonnementen");
        }

        [HttpGet]
        public ActionResult WijzigAbonnement(string naam)
        {
            List<Abonnement> abonnementen = abonnementRepository.ListAbonnementen();
            Abonnement abon = abonnementen.Find(abonnement => abonnement.Naam == naam);
            return View(abon);
        }

        [HttpPost]
        public ActionResult WijzigAbonnement(FormCollection form, string Naam)
        {
            string naam = Naam;
            double prijs = Convert.ToDouble(form["Prijs"]);
            string beschrijving = form["Beschrijving"];

            Abonnement abo = new Abonnement(naam, prijs, beschrijving);
            abonnementRepository.EditAbonnement(abo);
            return RedirectToAction("Abonnementen");
        }

        public ActionResult VerwijderAbonnement(string naam)
        {
            abonnementRepository.RemoveAbonnement(naam);
            return RedirectToAction("Abonnementen");
        }

        public ActionResult Scheldwoorden()
        {
            if (Session["Gebruiker"] != null)
            {
                Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
                if (gebruiker.Admin == true)
                {
                    List<Scheldwoord> scheldwoorden = scheldwoordRepository.ListScheldwoorden();
                    return View(scheldwoorden);
                }
                else
                {
                    return RedirectToAction("All", "Content");
                }
            }
            else
            {
                return RedirectToAction("All", "Content");
            }
        }

        [HttpGet]
        public ActionResult AddScheldwoord()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddScheldwoord(FormCollection form)
        {
            string woord = form["Woord"];
            Scheldwoord scheldwoord = new Scheldwoord(woord);
            scheldwoordRepository.AddScheldwoord(scheldwoord);
            return RedirectToAction("Scheldwoorden");
        }

        public ActionResult VerwijderScheldwoord(string woord)
        {
            scheldwoordRepository.RemoveScheldwoord(woord);
            return RedirectToAction("Scheldwoorden");
        }

        public ActionResult GebruikersTaalgebruik()
        {
            gebruikerRepository.CheckGebruikerstaal();
            List<Gebruiker> Gebruikers = gebruikerRepository.ListGebruikers();
            //Een nieuwe lijst wordt aangemaakt waarin de gebruikers komen met meer dan 1 bericht met ongepast taalgebruik
            //Die lijst wordt vervolgens meegegeven aan de View.
            List<Gebruiker> GebruikersAantal = new List<Gebruiker>();
            foreach (var item in Gebruikers)
            {
                if (item.Aantal >= ongepastaantal)
                {
                    GebruikersAantal.Add(item);
                }
            }
            return View(GebruikersAantal);
        }

        public ActionResult StuurMelding()
        {
            //In de session wordt de ontvanger gekozen die het bericht verstuurd naar alle gebruikers die te veel ongepast taalgebruik hebben gebruikt.
            Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
            List<Gebruiker> Gebruikers = gebruikerRepository.ListGebruikers();
            foreach (var item in Gebruikers)
            {
                //Kijkt of het aantal berichten meer of gelijk aan 2 is.
                if (item.Aantal >= ongepastaantal)
                {
                    berichtRepository.SendBericht(new Bericht(gebruiker, item, "Taalgebruik", "U heeft te vaak ongepast taalgebruik gebruikt. Als dit nogmaals gebeurt zal uw account verwijdert worden"));
                }
            }
            return RedirectToAction("Scheldwoorden");
        }

        public ActionResult OngepasteBerichten(int gebruikernr)
        {
            //Haalt als eerst de volledige lijst ongepaste berichten op om vervolgens de list te reversen zodat de nieuweste eerst komt
            //Vervolgens wordt er per bericht (getoond met huidig) de bericht toegevoegd aan de nieuwe lijst die getoond gaat worden in de View
            List<Bericht> berichten = berichtRepository.OngepasteBerichten(gebruikernr);
            berichten.Reverse();
            List<Bericht> LaatsteBerichten = new List<Bericht>();
            int huidig = 1;
            int aantalberichten = 3;
            while (huidig < aantalberichten)
            {
                LaatsteBerichten.Add(berichten[huidig]);
                huidig += 1;
            }
            return View(LaatsteBerichten);
        }
    }
}