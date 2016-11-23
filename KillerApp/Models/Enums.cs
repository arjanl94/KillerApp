using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KillerApp.Models
{
    public enum Geslacht
    {
        Man,
        Vrouw
    }

    public enum Genre
    {
        Horror,
        Comedy,
        Actie
    }

    public enum Resolutie
    {
        LowResolution,
        POne = 480,
        PTwo = 720,
        PThree = 1080
    }
}