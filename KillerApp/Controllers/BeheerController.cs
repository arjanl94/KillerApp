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

        public ActionResult Abonnementen()
        {
            List<Abonnement> abonnementen = abonnementRepository.ListAbonnementen();
            return View(abonnementen);
        }

        public ActionResult EditAbonnement(string naam)
        {
            List<Abonnement> abonnementen = abonnementRepository.ListAbonnementen();
            Abonnement abon = abonnementen.Find(abonnement => abonnement.Naam == naam);
            return View(abon);
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}