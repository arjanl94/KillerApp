using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class ContentRepository
    {
        private IContentServices _contentLogic;

        public ContentRepository(IContentServices contentLogic)
        {
            _contentLogic = contentLogic;
        }

        public List<Content> ListContent()
        {
            return _contentLogic.ListContent();
        }

        public List<Content> ListGebruikerContent(Gebruiker gebruiker)
        {
            return _contentLogic.ListGebruikerContent(gebruiker);
        }

        public void AddVideo(Video video)
        {
            _contentLogic.AddVideo(video);
        }

        public void AddMuziek(Muziek muziek)
        {
            _contentLogic.AddMuziek(muziek);
        }

        public void RemoveVideo(Video video)
        {
            _contentLogic.RemoveVideo(video);
        }

        public void RemoveMuziek(Muziek muziek)
        {
            _contentLogic.RemoveMuziek(muziek);
        }
    }
}