using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public interface IScheldwoordServices
    {
        List<Scheldwoord> ListScheldwoorden();
        void AddScheldwoord(Scheldwoord scheldwoord);
        void RemoveScheldwoord(Scheldwoord scheldwoord);
    }
}