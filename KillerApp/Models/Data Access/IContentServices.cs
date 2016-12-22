﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public interface IContentServices
    {
        List<Content> ListContent();
        List<Content> ListGebruikerContent(Gebruiker gebruiker);
        void AddVideo(Video video);
        void AddMuziek(Muziek muziek, Gebruiker uploader);
        void RemoveVideo(int videonr);
        void RemoveMuziek(Muziek muziek);
        Gebruiker SelectUploader(int gebruikernr);
    }
}