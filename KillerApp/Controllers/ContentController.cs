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
            try
            {
                List<Content> gebruikerscontent = contentRepository.ListGebruikerContent(gebruiker);
                return View(gebruikerscontent);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Er is geen content");
            }
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
            Genre genre = (Genre)Enum.Parse(typeof(Genre),form["Genre"]);
            string resolutie = form["Resolutie"];
            Video video = new Video(naam, beschrijving, duur, genre, uploader, resolutie);
            contentRepository.AddVideo(video);
            return RedirectToAction("UploadedContent");
        }

        public ActionResult VerwijderContent(int videonr)
        {
            contentRepository.RemoveVideo(videonr);
            return RedirectToAction("UploadedContent");
        }
    }
}