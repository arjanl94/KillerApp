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
            List<Content> content = contentRepository.ListContent();
            return View(content);
        }

        public ActionResult Videos()
        {
            List<Content> content = contentRepository.ListContent();
            List<Video> videos = content.Cast<Video>().ToList();
            return View(videos);
        }

        [HandleError]
        public ActionResult UploadedContent()
        {
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

    }
}