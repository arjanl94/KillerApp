using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerApp.Models;
using KillerApp.Models.Data_Access;

namespace KillerApp.Controllers
{
    public class UserController : Controller
    {
        GebruikerRepository gebruikerRepository = new GebruikerRepository(new MssqlGebruikerLogic());
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string email = form["Emailadres"];
            string wachtwoord = form["Wachtwoord"];
            Gebruiker gebruiker = gebruikerRepository.LoginGebruiker(email, wachtwoord);
            if (gebruiker != null)
            {
                Session["Gebruiker"] = gebruiker;
                return RedirectToAction("All", "Content");
            }
            else
            {
                ModelState.AddModelError("password", "Het emailadres of wachtwoord is verkeerd");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["Gebruiker"] = null;
            return RedirectToAction("All", "Content");
        }
    }
}