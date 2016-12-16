using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class AbonnementRepository
    {
        private IAbonnementServices _abonnementLogic;

        public AbonnementRepository(IAbonnementServices abonnementLogic)
        {
            _abonnementLogic = abonnementLogic;
        }

        public List<Abonnement> ListAbonnementen()
        {
            return _abonnementLogic.ListAbonnementen();
        }

        public void AddAbonnement(Abonnement abonnement)
        {
            _abonnementLogic.AddAbonnement(abonnement);
        }

        public void RemoveAbonnement(string naam)
        {
            _abonnementLogic.RemoveAbonnement(naam);
        }

        public void EditAbonnement(Abonnement abonnement)
        {
            _abonnementLogic.EditAbonnement(abonnement);
        }
    }
}