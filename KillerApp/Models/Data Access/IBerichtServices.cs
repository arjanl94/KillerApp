using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public interface IBerichtServices
    {
        List<Bericht> Berichten(Gebruiker gebruiker);
        void SendBericht(Bericht bericht);
        Gebruiker SelectGebruiker(int gebruikernr);
        void RemoveBericht(int berichtnr);
        List<Bericht> OngepasteBerichten(int gebruikernr);
    }
}