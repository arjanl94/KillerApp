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
        private const string Connectie =
            "Server=mssql.fhict.local;Database=dbi347556;User Id=dbi347556;Password=Qwerty1";

        //private const string Connectie =
        //    "Server=MSI;Database=KillerApp;Trusted_Connection=Yes;";

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
                            //Tabellen worden aan elkaar gekoppeld om zo de juiste gegevens uit de tabellen te kunnen halen om de objecten aan te kunnen maken.
                            cmd.CommandText =
                                "SELECT c.genretitel, c.uploader, c.Videonr, c.Muzieknr, c.likes, c.views, v.naam, v.beschrijving, v.duur, v.Resolutie, " +
                                "m.Naam, m.Beschrijving, m.Duur, m.kHz, c.Videonr, c.Muzieknr, c.Contentnr FROM content c left join video v ON c.Videonr = v.Videonr left join muziek m ON c.Muzieknr = m.Muzieknr";
                            cmd.Connection = conn;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Genre genre = (Genre)Enum.Parse(typeof(Genre), reader.GetString(0));
                                int uploadernr = reader.GetInt32(1);
                                int nr = reader.GetInt32(16);
                                Gebruiker uploader = SelectUploader(uploadernr);
                                //Als de derde kolom niet null is dan betekent het dat het een video gaat. Daarom is er een if statement toegevoegd
                                //Dat de juiste gegevens ophaalt mocht er een video toegevoegd worden.
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
                                    int videonr = reader.GetInt32(14);
                                    Content.Add(new Video(nr, naam, beschrijving, duur, genre, uploader, resolutie, videonr));
                                }
                                //Kijkt of de vierde kolom niet leeg is. 
                                //Als dit het geval is betreft het een muziek bestand en wordt er een nieuw Muziek object aangemaakt.
                                else if (!reader.IsDBNull(3))
                                {
                                    string naam = reader.GetString(10);
                                    string beschrijving = "Leeg";
                                    if (!reader.IsDBNull(11))
                                    {
                                        beschrijving = reader.GetString(11);
                                    }
                                    TimeSpan duur = reader.GetTimeSpan(12);
                                    int khz = reader.GetInt32(13);
                                    int muzieknr = reader.GetInt32(15);
                                    Content.Add(new Muziek(nr, naam, beschrijving, duur, genre, uploader, khz, muzieknr));
                                }
                            }
                            return Content;
                        }
                        catch (Exception)
                        {
                            return null;
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
                            cmd.CommandText =
                                "SELECT c.genretitel, c.uploader, c.Videonr, c.Muzieknr, c.likes, c.views, v.naam, v.beschrijving, v.duur, v.Resolutie, " +
                                "m.Naam, m.Beschrijving, m.Duur, m.kHz, c.Videonr, c.Muzieknr, c.Contentnr FROM content c left join video v ON c.Videonr = v.Videonr left join muziek m ON c.Muzieknr = m.Muzieknr " +
                                "WHERE c.uploader = @uploader";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@uploader", gebruiker.Gebruikernr);

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Genre genre = (Genre)Enum.Parse(typeof(Genre), reader.GetString(0));
                                int uploadernr = reader.GetInt32(1);
                                int nr = reader.GetInt32(16);
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
                                    int videonr = reader.GetInt32(14);
                                    Content.Add(new Video(nr, naam, beschrijving, duur, genre, uploader, resolutie, videonr));
                                }
                                else if (!reader.IsDBNull(3))
                                {
                                    string naam = reader.GetString(10);
                                    string beschrijving = "Leeg";
                                    if (!reader.IsDBNull(11))
                                    {
                                        beschrijving = reader.GetString(11);
                                    }
                                    TimeSpan duur = reader.GetTimeSpan(12);
                                    int khz = reader.GetInt32(13);
                                    int muzieknr = reader.GetInt32(15);
                                    Content.Add(new Muziek(nr, naam, beschrijving, duur, genre, uploader, khz, muzieknr));
                                }
                            }
                            return Content;
                        }
                        catch (Exception)
                        {
                            return null;
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

        public void AddVideo(Video video)
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
                            cmd.CommandText = "EXECUTE VideoContent @naam, @duur, @resolutie, @genretitel, @uploader, @beschrijving";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", video.Naam);
                            cmd.Parameters.AddWithValue("@beschrijving", video.Beschrijving);
                            cmd.Parameters.AddWithValue("@duur", video.Duur);
                            if (video.Resolutie == Resolutie._1080P)
                            {
                                cmd.Parameters.AddWithValue("@resolutie", "1080P");
                            }
                            else if (video.Resolutie == Resolutie._720P)
                            {
                                cmd.Parameters.AddWithValue("@resolutie", "720P");
                            }
                            else if (video.Resolutie == Resolutie._480P)
                            {
                                cmd.Parameters.AddWithValue("@resolutie", "480P");
                            }
                            else if (video.Resolutie == Resolutie.LowResolution)
                            {
                                cmd.Parameters.AddWithValue("@resolutie", "Low Resolution");
                            }
                            cmd.Parameters.AddWithValue("@genretitel", video.Genre.ToString());
                            cmd.Parameters.AddWithValue("@uploader", video.Uploader.Gebruikernr);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
        }

        public void AddMuziek(Muziek muziek)
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
                            cmd.CommandText = "EXECUTE MuziekContent @naam, @duur, @khz, @genretitel, @uploader, @beschrijving";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", muziek.Naam);
                            cmd.Parameters.AddWithValue("@beschrijving", muziek.Beschrijving);
                            cmd.Parameters.AddWithValue("@duur", muziek.Duur);
                            cmd.Parameters.AddWithValue("@khz", muziek.kHz);
                            cmd.Parameters.AddWithValue("@genretitel", muziek.Genre.ToString());
                            cmd.Parameters.AddWithValue("@uploader", muziek.Uploader.Gebruikernr);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
        }

        public void RemoveVideo(int videonr)
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
                            //Een procedure is toegevoegd aan de database waarbij zowel de video als content invoer verwijdert wordt.
                            cmd.CommandText = "EXECUTE RemoveVideoContent @videonr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@videonr", videonr);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
        }

        public void RemoveMuziek(int muzieknr)
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
                            cmd.CommandText = "EXECUTE RemoveMuziekContent @muzieknr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@muzieknr", muzieknr);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
        }

        //Bij het aanmaken van een video of muziek object is er een uploader nodig. Vandaar dat dit een aparte methode is geworden 
        //Waarin de juiste gebruiker wordt opgezocht aan de hand van de gebruikernr dat aanwezig is bij de content in de database.
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
                            Geslacht geslacht = (Geslacht)Enum.Parse(typeof(Geslacht), reader.GetString(4));
                            string email = reader.GetString(5);
                            string wachtwoord = reader.GetString(6);
                            int aantal = reader.GetInt32(7);
                            return new Gebruiker(gebrnr, abonnement, naam, gebruikersnaam, geslacht, email, wachtwoord, aantal);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
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

        //Selecteer de juiste video wat gebruikt gaat worden om de content van de video te laten zien met de bijbehorende comments(indien aanwezig)
        public Video SelectVideo(int contentnr)
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
                                "SELECT c.genretitel, c.uploader, c.Videonr, c.Muzieknr, c.likes, c.views, v.naam, v.beschrijving, v.duur, v.Resolutie, m.Naam, " +
                                "m.Beschrijving, m.Duur, m.kHz, c.Videonr, c.Muzieknr, c.Contentnr FROM content c left join video v ON c.Videonr = v.Videonr " +
                                "left join muziek m ON c.Muzieknr = m.Muzieknr WHERE c.Contentnr = @nr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@nr", contentnr);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                Genre genre = (Genre)Enum.Parse(typeof(Genre), reader.GetString(0));
                                int uploadernr = reader.GetInt32(1);
                                int nr = reader.GetInt32(16);
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
                                    int vidnr = reader.GetInt32(14);
                                    return new Video(nr, naam, beschrijving, duur, genre, uploader, resolutie, vidnr);
                                }
                                else if (!reader.IsDBNull(3))
                                {
                                    return null;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            return null;
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

        //Selecteer de juiste muziek wat gebruikt wordt om de content te laten zien met bijbehorende comments (indien aanwezig)
        public Muziek SelectMuziek(int contentnr)
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
                                "SELECT c.genretitel, c.uploader, c.Videonr, c.Muzieknr, c.likes, c.views, v.naam, v.beschrijving, v.duur, v.Resolutie, m.Naam, " +
                                "m.Beschrijving, m.Duur, m.kHz, c.Videonr, c.Muzieknr, c.Contentnr FROM content c left join video v ON c.Videonr = v.Videonr " +
                                "left join muziek m ON c.Muzieknr = m.Muzieknr WHERE c.Contentnr = @nr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@nr", contentnr);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                Genre genre = (Genre)Enum.Parse(typeof(Genre), reader.GetString(0));
                                int uploadernr = reader.GetInt32(1);
                                int nr = reader.GetInt32(16);
                                Gebruiker uploader = SelectUploader(uploadernr);
                                if (!reader.IsDBNull(2))
                                {
                                    return null;
                                }
                                else if (!reader.IsDBNull(3))
                                {
                                    string naam = reader.GetString(10);
                                    string beschrijving = "Leeg";
                                    if (!reader.IsDBNull(11))
                                    {
                                        beschrijving = reader.GetString(11);
                                    }
                                    TimeSpan duur = reader.GetTimeSpan(12);
                                    int khz = reader.GetInt32(13);
                                    int muzieknr = reader.GetInt32(15);
                                    return new Muziek(nr, naam, beschrijving, duur, genre, uploader, khz, muzieknr);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            return null;
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