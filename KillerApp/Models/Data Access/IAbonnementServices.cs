using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public interface IAbonnementServices
    {
        List<Abonnement> ListAbonnementen();
        void AddAbonnement(Abonnement abonnement);
        void RemoveAbonnement(string naam);
        void EditAbonnement(Abonnement abonnement);
    }
}