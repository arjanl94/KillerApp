using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class GebruikerRepository
    {
        private IGebruikerServices _gebruikerLogic;

        public GebruikerRepository(IGebruikerServices gebruikerLogic)
        {
            _gebruikerLogic = gebruikerLogic;
        }

        public List<Gebruiker> ListGebruikers()
        {
            return _gebruikerLogic.ListGebruikers();
        }

        public void AddGebruiker(Gebruiker gebruiker)
        {
            _gebruikerLogic.AddGebruiker(gebruiker);
        }

        public void RemoveGebruiker(int gebruikernr)
        {
            _gebruikerLogic.RemoveGebruiker(gebruikernr);
        }

        public void EditGebruiker(Gebruiker gebruiker)
        {
            _gebruikerLogic.EditGebruiker(gebruiker);
        }

        public Gebruiker LoginGebruiker(string email, string wachtwoord)
        {
            return _gebruikerLogic.LoginGebruiker(email, wachtwoord);
        }

        public Gebruiker GebruikerByEmail(string email)
        {
            return _gebruikerLogic.GebruikerByEmail(email);
        }
    }
}