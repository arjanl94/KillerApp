using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerApp.Models.Data_Access;

namespace KillerApp.Models
{
    public class BerichtGebruikerView
    {
        private GebruikerRepository gebruikerRepository = new GebruikerRepository(new MssqlGebruikerLogic());
        public Bericht Bericht { get; set; }
        public Gebruiker Gebruiker { get; set; }

        public BerichtGebruikerView(Bericht bericht)
        {
            this.Bericht = bericht;
        }
        public BerichtGebruikerView()
        {

        }
    }
}