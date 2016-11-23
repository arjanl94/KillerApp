using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class MssqlContentLogic : IContentServices
    {
        public List<Content> ListContent()
        {
            throw new NotImplementedException();
        }

        public List<Content> ListGebruikerContent(Gebruiker gebruiker)
        {
            throw new NotImplementedException();
        }

        public void AddVideo(Video video)
        {
            throw new NotImplementedException();
        }

        public void AddMuziek(Muziek muziek)
        {
            throw new NotImplementedException();
        }

        public void RemoveVideo(Video video)
        {
            throw new NotImplementedException();
        }

        public void RemoveMuziek(Muziek muziek)
        {
            throw new NotImplementedException();
        }
    }
}