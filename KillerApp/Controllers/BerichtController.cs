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
        private ContentRepository contentRepository = new ContentRepository(new MssqlContentLogic());
        private GebruikerRepository gebruikerRepository = new GebruikerRepository(new MssqlGebruikerLogic());
        // GET: Bericht
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inbox()
        {
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
            return View();
        }
    }
}