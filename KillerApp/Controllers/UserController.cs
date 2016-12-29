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
            //Uit de form wordt de ingevoerde emailadres en wachtwoord gemaakt. Dit wordt vervolgens in de repository gecontroleerd.
            //Als de gegevens kloppen wordt een Gebruiker gereturned en wordt dit in 'gebruiker' gezet
            Gebruiker gebruiker = gebruikerRepository.LoginGebruiker(email, wachtwoord);
            //Als gebruiker niet null is dan wordt de hoofdpagina getoond
            if (gebruiker != null)
            {
                //De gebruiker wordt in de Session "Gebruiker" gezet. Nu is het mogelijk om in de gehele session deze gebruiker weer op te halen
                Session["Gebruiker"] = gebruiker;
                return RedirectToAction("All", "Content");
            }
            //Als er een fout is met het ophalen van de gebruiker en de gebruiker dus null is wordt een login fout getoond
            else
            {
                ModelState.AddModelError("password", "Het emailadres of wachtwoord is verkeerd");
            }
            return View();
        }

        public ActionResult Logout()
        {
            //Maakt de Session leeg zodat de systeem weet dat er niemand is ingelogd. Vervolgens wordt de hoofdpagina weer getoond.
            Session["Gebruiker"] = null;
            return RedirectToAction("All", "Content");
        }
    }
}