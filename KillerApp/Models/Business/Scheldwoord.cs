using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Business
{
    public class Scheldwoord
    {
        public string Woord { get; set; }

        public Scheldwoord(string woord)
        {
            Woord = woord;
        }
    }
}