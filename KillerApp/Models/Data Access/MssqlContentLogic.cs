using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace KillerApp.Models.Data_Access
{
    public class MssqlContentLogic : IContentServices
    {
        //Connectiestring met database
        //private const string Connectie =
        //    "Server=mssql.fhict.local;Database=dbi347556;User Id=dbi347556;Password=Qwerty1";

        private const string Connectie =
            "Server=MSI;Database=KillerApp;Trusted_Connection=Yes;";

        public List<Content> ListContent()
        {
            using (SqlConnection conn = new SqlConnection(Connectie))
            {
                List<Content> Content = new List<Content>();

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "SELECT c.genretitel, c.uploader, c.Videonr, c.Muzieknr, c.likes, c.views, v.naam, v.beschrijving, v.duur, v.Resolutie, " +
                                              "m.Naam, m.Beschrijving, m.Duur, m.kHz FROM content c left join video v ON c.Videonr = v.Videonr left join muziek m ON c.Muzieknr = m.Muzieknr";
                            cmd.Connection = conn;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Genre genre = (Genre)Enum.Parse(typeof(Genre), reader.GetString(0));
                                int uploadernr = reader.GetInt32(1);
                                Gebruiker uploader = SelectUploader(uploadernr);
                                if (!reader.IsDBNull(2))
                                {
                                    string naam = reader.GetString(6);
                                    string beschrijving = "Leeg";
                                    if (!reader.IsDBNull(7))
                                    {
                                        beschrijving = reader.GetString(7);
                                    }
                                    TimeSpan duur = reader.GetTimeSpan(8);
                                    string resolutie = reader.GetString(9);
                                    Content.Add(new Video(naam, beschrijving, duur, genre, uploader, resolutie));
                                }
                                else if (!reader.IsDBNull(3))
                                {

                                }
                            }
                            return Content;
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
            return null;
        }

        public List<Content> ListGebruikerContent(Gebruiker gebruiker)
        {
            throw new NotImplementedException();
        }

        public void AddVideo(Video video)
        {
            throw new NotImplementedException();
        }

        public void AddMuziek(Muziek muziek)
        {
            throw new NotImplementedException();
        }

        public void RemoveVideo(Video video)
        {
            throw new NotImplementedException();
        }

        public void RemoveMuziek(Muziek muziek)
        {
            throw new NotImplementedException();
        }

        public Gebruiker SelectUploader(int gebruikernr)
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
                            cmd.CommandText = "SELECT * FROM Gebruiker WHERE Gebruikernr = @gebruikernr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@gebruikernr", gebruikernr);

                            SqlDataReader reader = cmd.ExecuteReader();
                            reader.Read();
                            int gebrnr = reader.GetInt32(0);
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
                            return new Gebruiker(gebrnr, abonnement, naam, gebruikersnaam, geslacht, email, wachtwoord);
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
            return null;
        }
    }
}