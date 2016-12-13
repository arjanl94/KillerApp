using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class BerichtRepository
    {
        private IBerichtServices _berichtLogic;

        public BerichtRepository(IBerichtServices berichtLogic)
        {
            _berichtLogic = berichtLogic;
        }

        public List<Bericht> Berichten(Gebruiker gebruiker)
        {
            return _berichtLogic.Berichten(gebruiker);
        }

        public void SendBericht(Bericht bericht)
        {
            _berichtLogic.SendBericht(bericht);
        }

        public Gebruiker SelectGebruiker(int gebruikernr)
        {
            return _berichtLogic.SelectGebruiker(gebruikernr);
        }
    }
}