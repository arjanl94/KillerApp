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

        public List<Bericht> Berichten()
        {
            return _berichtLogic.Berichten();
        }

        public void SendBericht(Bericht bericht)
        {
            _berichtLogic.SendBericht(bericht);
        }
    }
}