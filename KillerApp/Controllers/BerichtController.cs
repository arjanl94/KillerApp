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
        // GET: Bericht
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inbox()
        {
            Gebruiker gebruiker = berichtRepository.SelectGebruiker(2);
            List<Bericht> berichten = berichtRepository.Berichten(gebruiker);
            return View(berichten);
        }

        public ActionResult Details(int berichtnr)
        {
            Gebruiker gebruiker = berichtRepository.SelectGebruiker(2);
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
            return View();
        }

        [HttpPost]
        public ActionResult NieuwBericht(FormCollection form)
        {
            Gebruiker verzender = contentRepository.SelectUploader(1);
            Gebruiker ontvanger = contentRepository.SelectUploader(2);
            string titel = form["Titel"];
            string tekst = form["Tekst"];
            Bericht bericht = new Bericht(verzender, ontvanger, titel, tekst);
            berichtRepository.SendBericht(bericht);
            return View();
        }
    }
}