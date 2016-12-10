using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using Microsoft.Win32.SafeHandles;

namespace KillerApp.Models.Data_Access
{
    public class MssqlGebruikerLogic : IGebruikerServices
    {
        //Connectiestring met database
        private const string Connectie =
            "Server=mssql.fhict.local;Database=dbi347556;User Id=dbi347556;Password=Qwerty1";

        //Haalt een lijst met alle gebruikers op
        public List<Gebruiker> ListGebruikers()
        {
            using (SqlConnection conn = new SqlConnection(Connectie))
            {
                List<Gebruiker> gebruikers = new List<Gebruiker>();
                //Als de connectie nog niet open is, wordt hij hier open gezet
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    //Nieuw command
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            //Query
                            cmd.CommandText = "SELECT * FROM Gebruiker";
                            cmd.Connection = conn;

                            //Nieuwe reader aanmaken
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                //Voor iedere kolom die hij leest, geeft hij de waarde van die kolom aan het volgende. 
                                //De kolom wordt gekozen door middel van (kolom) aan het eind.
                                string abonnement = "null";
                                string naam = "null";
                                string gebruikersnaam = "null";

                                if (!reader.IsDBNull(1))
                                {
                                    abonnement = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    naam = reader.GetString(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    gebruikersnaam = reader.GetString(3);
                                }
                                //Geslacht geslacht = (Geslacht)Enum.Parse(typeof(Geslacht), reader.GetString(4));
                                Geslacht geslacht = Geslacht.Man;
                                string email = reader.GetString(5);
                                string wachtwoord = reader.GetString(6);
                                gebruikers.Add(new Gebruiker(abonnement, naam, gebruikersnaam, geslacht, email, wachtwoord));
                            }
                            //Retourneert de lijst met gebruikers
                            return gebruikers;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            //Het sluiten van de verbinding met de database.
                            conn.Close();
                        }
                    }
                }
                //Retourneert niks als het niet lukt
                return null;
            }
        }
        //Voegt een gebruiker toe
        public void AddGebruiker(Gebruiker gebruiker)
        {
            using (SqlConnection conn = new SqlConnection(Connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText =
                                "INSERT INTO Gebruiker (Naam, Gebruikersnaam, Geslacht, Emailadres, Wachtwoord) VALUES (@naam, @gebruikersnaam, @geslacht, @emailadres, @wachtwoord";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", gebruiker.Naam);
                            cmd.Parameters.AddWithValue("@gebruikersnaam", gebruiker.Gebruikersnaam);
                            cmd.Parameters.AddWithValue("@geslacht", gebruiker.Geslacht);
                            cmd.Parameters.AddWithValue("@emailadres", gebruiker.Emailadres);
                            cmd.Parameters.AddWithValue("@wachtwoord", gebruiker.Wachtwoord);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        public void RemoveGebruiker(Gebruiker gebruiker)
        {
            throw new NotImplementedException();
        }

        public void EditGebruiker(Gebruiker gebruiker)
        {
            throw new NotImplementedException();
        }

        public Gebruiker CheckForGebruiker(Gebruiker gebruiker)
        {
            throw new NotImplementedException();
        }
    }
}