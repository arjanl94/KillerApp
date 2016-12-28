using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public interface IReactieServices
    {
        List<Reactie> ListContentReacties(int contentnr);
        void AddReactie(Reactie reactie);
        Gebruiker SelectGebruiker(int gebruikernr);
    }
}