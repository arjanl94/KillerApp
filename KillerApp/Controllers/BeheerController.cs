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
        // GET: Beheer
        
        public ActionResult Gebruikers()
        {
            if (Session["Gebruiker"] != null)
            {
                Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
                if (gebruiker.Admin == true)
                {
                    List<Gebruiker> gebruikers = gebruikerRepository.ListGebruikers();
                    return View(gebruikers);
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
        public ActionResult AddGebruiker()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGebruiker(FormCollection form)
        {
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
            List<Gebruiker> gebruikers = gebruikerRepository.ListGebruikers();
            Gebruiker user = gebruikers.Find(gebruiker => gebruiker.Emailadres == email);
            return View(user);
        }

        [HttpPost]
        public ActionResult WijzigGebruiker(FormCollection form, string email)
        {
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
            gebruikerRepository.RemoveGebruiker(Gebruikernr);
            return RedirectToAction("Gebruikers");
        }

        public ActionResult Abonnementen()
        {
            if (Session["Gebruiker"] != null)
            {
                Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
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

        public ActionResult Details()
        {
            return View();
        }
    }
}