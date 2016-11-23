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

        public List<Reactie> ListContentReacties()
        {
            return _reactieLogic.ListContentReacties();
        }

        public void AddReactie(Reactie reactie)
        {
            _reactieLogic.AddReactie(reactie);
        }
    }
}