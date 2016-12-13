using System;
using System.Collections.Generic;
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
        // GET: Content
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All()
        {
            List<Content> content = contentRepository.ListContent();
            return View(content);
        }
    }
}