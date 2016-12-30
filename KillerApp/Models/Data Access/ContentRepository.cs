﻿using System;
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

        public void RemoveVideo(int videonr)
        {
            _contentLogic.RemoveVideo(videonr);
        }

        public void RemoveMuziek(int muzieknr)
        {
            _contentLogic.RemoveMuziek(muzieknr);
        }

        public Gebruiker SelectUploader(int gebruikernr)
        {
            return _contentLogic.SelectUploader(gebruikernr);
        }

        public Video SelectVideo(int contentnr)
        {
            return _contentLogic.SelectVideo(contentnr);
        }

        public Muziek SelectMuziek(int contentnr)
        {
            return _contentLogic.SelectMuziek(contentnr);
        }

        public void MeldingUpload(int gebruikernr, string titel)
        {
            _contentLogic.MeldingUpload(gebruikernr, titel);
        }
    }
}