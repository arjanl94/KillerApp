using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerApp.Models;
using KillerApp.Models.Data_Access;

namespace KillerApp.Controllers
{
    public class BerichtController : Controller
    {
        private BerichtRepository berichtRepository = new BerichtRepository(new MssqlBerichtLogic());
        private GebruikerRepository gebruikerRepository = new GebruikerRepository(new MssqlGebruikerLogic());
        // GET: Bericht
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inbox()
        {
            //Als eerst wordt er gekeken of een gebruiker is ingelogd. Mocht dit niet het geval zijn wordt het terug gebracht 
            //naar het contentoverzicht
            if (Session["Gebruiker"] !=null)
            {
                Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
                List<Bericht> berichten = berichtRepository.Berichten(gebruiker);
                return View(berichten);
            }
            else
            {
                return RedirectToAction("All", "Content");
            }
        }

        public ActionResult Details(int berichtnr)
        {
            //Het juiste bericht wordt gekozen aan de hand van de gebruiker en de berichtnr
            //Als de gebruiker niet is ingelogd is het dus ook niet mogelijk om naar het bericht te gaan met alleen het berichtnr(extra beveiliging)
            Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
            List<Bericht> berichten = berichtRepository.Berichten(gebruiker);
            Bericht msg = berichten.Find(bericht => bericht.Berichtnr == berichtnr);
            return View(msg);
        }

        public ActionResult VerwijderBericht(int berichtnr)
        {
            berichtRepository.RemoveBericht(berichtnr);
            return RedirectToAction("Inbox");
        }

        [HttpGet]
        public ActionResult NieuwBericht()
        {
            //Een model is aangemaakt waarin een Bericht en gebruiker gebruikt kan worden in de view
            BerichtGebruikerView berichtGebruiker = new BerichtGebruikerView();
            return View(berichtGebruiker);
        }

        [HttpPost]
        public ActionResult NieuwBericht(FormCollection form)
        {
            Gebruiker verzender = Session["Gebruiker"] as Gebruiker;
            string email = form["Emailadres"];
            Gebruiker ontvanger = gebruikerRepository.GebruikerByEmail(email);
            string titel = form["Titel"];
            string tekst = form["Tekst"];
            Bericht bericht = new Bericht(verzender, ontvanger, titel, tekst);
            berichtRepository.SendBericht(bericht);
            return RedirectToAction("Inbox");
        }
    }
}