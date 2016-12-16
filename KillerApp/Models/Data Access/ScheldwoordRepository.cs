using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class ScheldwoordRepository
    {
        private IScheldwoordServices _scheldwoordLogic;

        public ScheldwoordRepository(IScheldwoordServices scheldwoordLogic)
        {
            _scheldwoordLogic = scheldwoordLogic;
        }

        public List<Scheldwoord> ListScheldwoorden()
        {
            return _scheldwoordLogic.ListScheldwoorden();
        }

        public void AddScheldwoord(Scheldwoord scheldwoord)
        {
            _scheldwoordLogic.AddScheldwoord(scheldwoord);
        }

        public void RemoveScheldwoord(string scheldwoord)
        {
            _scheldwoordLogic.RemoveScheldwoord(scheldwoord);
        }
    }
}