using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class ReactieRepository
    {
        private IReactieServices _reactieLogic;

        public ReactieRepository(IReactieServices reactieLogic)
        {
            _reactieLogic = reactieLogic;
        }

        public List<Reactie> ListContentReacties(int contentnr)
        {
            return _reactieLogic.ListContentReacties(contentnr);
        }

        public void AddReactie(Reactie reactie)
        {
            _reactieLogic.AddReactie(reactie);
        }

        public Gebruiker SelectGebruiker(int gebruikernr)
        {
            return _reactieLogic.SelectGebruiker(gebruikernr);
        }
    }
}