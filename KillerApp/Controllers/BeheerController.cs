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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gebruikers()
        {
            List<Gebruiker> gebruikers = gebruikerRepository.ListGebruikers();
            return View(gebruikers);
        }
        public ActionResult WijzigGebruiker(string email)
        {
            List<Gebruiker> gebruikers = gebruikerRepository.ListGebruikers();
            Gebruiker user = gebruikers.Find(gebruiker => gebruiker.Emailadres == email);
            return View(user);
        }

        public ActionResult Abonnementen()
        {
            List<Abonnement> abonnementen = abonnementRepository.ListAbonnementen();
            return View(abonnementen);
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
            abonnementRepository.AddAbonnement(abo);
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
            List<Scheldwoord> scheldwoorden = scheldwoordRepository.ListScheldwoorden();
            return View(scheldwoorden);
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