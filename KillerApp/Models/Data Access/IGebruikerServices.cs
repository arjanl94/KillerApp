using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public interface IGebruikerServices
    {
        List<Gebruiker> ListGebruikers();
        void AddGebruiker(Gebruiker gebruiker);
        void RemoveGebruiker(Gebruiker gebruiker);
        void EditGebruiker(Gebruiker gebruiker);
        Gebruiker LoginGebruiker(string email, string wachtwoord);
        Gebruiker GebruikerByEmail(string email);
    }
}