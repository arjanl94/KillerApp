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
            //IEnumerable<SelectListItem> selectAbonnement =
            //    from a in abonnementen
            //    select new SelectListItem
            //    {
            //        Text = a.Naam,
            //        Value = a.ToString()
            //    };
            return View(abonnementen);
        }
    }
}