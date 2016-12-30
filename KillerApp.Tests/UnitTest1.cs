using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KillerApp.Controllers;
using KillerApp.Models;

namespace KillerApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLogin()
        {
            UserController controller = new UserController();
            var result = controller.Login() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void AllContent()
        {
            ContentController controller = new ContentController();
            ViewResult result = controller.All() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UserContent()
        {
            ContentController controller = new ContentController();
            ViewResult result = controller.UploadedContent() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddGebruiker()
        {
            BeheerController controller = new BeheerController();
            ViewResult result = controller.AddGebruiker() as ViewResult;
            ///   Assert.IsNotNull(result);
            Assert.AreEqual(null, result.Model);
        }

        [TestMethod]
        public void AddScheldwoord()
        {
            BeheerController controller = new BeheerController();
            ViewResult result = controller.AddScheldwoord() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WijzigGebruiker()
        {
            BeheerController controller = new BeheerController();
            ViewResult result = controller.WijzigGebruiker("Admin@gmail.com") as ViewResult;
            Assert.AreEqual("Naam: Admin", result.Model.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UploadedContent()
        {
            ContentController controller = new ContentController();
            ViewResult result = controller.UploadedContent() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void View()
        {
            ContentController controller = new ContentController();
            ViewResult result = controller.View(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Upload()
        {
            ContentController controller = new ContentController();
            ViewResult result = controller.Upload() as ViewResult;
            Assert.IsNotNull(result);
        }

    }

    [TestClass]
    public class BerichtTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Inbox()
        {
            BerichtController controller = new BerichtController();
            ViewResult result = controller.Inbox() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DetailsBericht()
        {
            BerichtController controller = new BerichtController();
            ViewResult result = controller.Details(1) as ViewResult;
            Assert.IsNotNull(result);
    }


}
}
