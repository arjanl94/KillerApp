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
    }
}