using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerApp.Models;
using KillerApp.Models.Data_Access;

namespace KillerApp.Controllers
{
    public class ContentController : Controller
    {
        private ContentRepository contentRepository = new ContentRepository(new MssqlContentLogic());
        private ReactieRepository reactieRepository = new ReactieRepository(new MssqlReactieLogic());

        public ActionResult All()
        {
            //Haalt de lijst met alle content op
            try
            {
                List<Content> content = contentRepository.ListContent();
                //Draait de volgorde van de content zodat de nieuwest bovenaan staat.
                content.Reverse();
                return View(content);
            }
            catch (Exception ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        public ActionResult Videos()
        {
            try
            {
                List<Content> content = contentRepository.ListContent();
                List<Video> videos = content.Cast<Video>().ToList();
                return View(videos);
            }
            catch (Exception ex)
            {
                throw new HttpException(ex.Message);
            }
        }

        [HandleError]
        public ActionResult UploadedContent()
        {
            //Haalt de gebruiker uit de Session om vervolgens te gebruiken om de lijst met eigen content te tonen
            Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;

            List<Content> gebruikerscontent = contentRepository.ListGebruikerContent(gebruiker);
            return View(gebruikerscontent);

        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(FormCollection form)
        {
            //Alle gegevens worden opgehaald uit de form om vervolgens een video aan de database te kunnen toevoegen
            Gebruiker uploader = Session["Gebruiker"] as Gebruiker;
            string naam = form["Naam"];
            string beschrijving = form["Beschrijving"];
            TimeSpan duur = TimeSpan.Parse(form["Duur"]);
            Genre genre = (Genre)Enum.Parse(typeof(Genre), form["Genre"]);
            string resolutie = form["Resolutie"];
            Video video = new Video(naam, beschrijving, duur, genre, uploader, resolutie);
            contentRepository.AddVideo(video);
            return RedirectToAction("UploadedContent");
        }

        [HttpGet]
        public ActionResult UploadMuziek()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadMuziek(FormCollection form)
        {
            Gebruiker uploader = Session["Gebruiker"] as Gebruiker;
            string naam = form["Naam"];
            string beschrijving = form["Beschrijving"];
            TimeSpan duur = TimeSpan.Parse(form["Duur"]);
            Genre genre = (Genre) Enum.Parse(typeof(Genre), form["Genre"]);
            int khz = Convert.ToInt32(form["kHz"]);
            Muziek muziek = new Muziek(naam, beschrijving, duur, genre, uploader, khz);
            contentRepository.AddMuziek(muziek);
            return RedirectToAction("UploadedContent");
        }

        public ActionResult VerwijderContent(int contentnr)
        {
            //Verwijdert de content uit de database aan de hand van de contentnr dat aan de Action wordt meegegeven. De gebruiker wordt vervolgens
            //Terug gebracht naar zijn/haar lijst met geüploade content
            contentRepository.RemoveVideo(contentnr);
            return RedirectToAction("UploadedContent");
        }

        public ActionResult VerwijderMuziek(int contentnr)
        {
            contentRepository.RemoveMuziek(contentnr);
            return RedirectToAction("UploadedContent");
        }

        [HttpGet]
        public ActionResult View(int contentnr)
        {
            //De reacties worden opgehaald aan de hand van de contentnr, de lijst met reacties wordt vervolgens omgedraaid zodat 
            //het nieuwste bericht bovenaan staat.
            List<Reactie> reacties = reactieRepository.ListContentReacties(contentnr);
            reacties.Reverse();
            Video video = contentRepository.SelectVideo(contentnr);
            video.AddReacties(reacties);
            return View(video);
        }

        [HttpGet]
        public ActionResult View_m(int contentnr)
        {
            //De reacties worden opgehaald aan de hand van de contentnr, de lijst met reacties wordt vervolgens omgedraaid zodat 
            //het nieuwste bericht bovenaan staat.
            List<Reactie> reacties = reactieRepository.ListContentReacties(contentnr);
            reacties.Reverse();
            Muziek muziek = contentRepository.SelectMuziek(contentnr);
            muziek.AddReacties(reacties);
            return View(muziek);
        }

        [HttpPost]
        public ActionResult View(FormCollection form)
        {
            //Hier wordt een nieuw bericht toegevoegd. Als geen gebruiker is ingelogd is het niet mogelijk om een bericht te plaatsen
            Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
            int contentnr = Convert.ToInt32(form["Nr"]);
            string tekst = form["Reactie"];
            Reactie reactie = new Reactie(gebruiker, tekst, contentnr);
            try
            {
                reactieRepository.AddReactie(reactie);
            }
            catch
            {
                RedirectToAction("All", "Content");
            }
            
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult View_m(FormCollection form)
        {
            Gebruiker gebruiker = Session["Gebruiker"] as Gebruiker;
            int contentnr = Convert.ToInt32(form["Nr"]);
            string tekst = form["Reactie"];
            Reactie reactie = new Reactie(gebruiker, tekst, contentnr);
            try
            {
                reactieRepository.AddReactie(reactie);
            }
            catch
            {
                RedirectToAction("All", "Content");
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}