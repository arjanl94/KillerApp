using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public interface IBerichtServices
    {
        List<Bericht> Berichten();
        void SendBericht(Bericht bericht);
    }
}