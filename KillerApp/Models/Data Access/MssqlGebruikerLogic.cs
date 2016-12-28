using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using Microsoft.Owin.Security.Provider;
using Microsoft.Win32.SafeHandles;

namespace KillerApp.Models.Data_Access
{
    public class MssqlGebruikerLogic : IGebruikerServices
    {
        //Connectiestring met database
        private const string Connectie =
            "Server=mssql.fhict.local;Database=dbi347556;User Id=dbi347556;Password=Qwerty1";
        
        //    "Server=MSI;Database=KillerApp;Trusted_Connection=Yes;";

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
                                gebruikers.Add(new Gebruiker(gebrnr, abonnement, naam, gebruikersnaam, geslacht, email, wachtwoord));
                            }
                            //Retourneert de lijst met gebruikers
                            return gebruikers;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
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
                        // Als alleen een gebruikersnaam is ingevoerd
                        if (gebruiker.Naam == null && gebruiker.Gebruikersnaam != null)
                        {
                            try
                            {
                                cmd.CommandText =
                                    "INSERT INTO Gebruiker (Gebruikersnaam, Geslacht, Emailadres, Wachtwoord) VALUES (@gebruikersnaam, @geslacht, @emailadres, @wachtwoord)";
                                cmd.Connection = conn;
                                
                                cmd.Parameters.AddWithValue("@gebruikersnaam", gebruiker.Gebruikersnaam);
                                cmd.Parameters.AddWithValue("@geslacht", gebruiker.Geslacht.ToString());
                                cmd.Parameters.AddWithValue("@emailadres", gebruiker.Emailadres);
                                cmd.Parameters.AddWithValue("@wachtwoord", gebruiker.Wachtwoord);

                                cmd.ExecuteNonQuery();
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
                        // Als alleen een naam is ingevoerd
                        else if (gebruiker.Naam != null && gebruiker.Gebruikersnaam == null)
                        {
                            try
                            {
                                cmd.CommandText =
                                    "INSERT INTO Gebruiker (Naam, Geslacht, Emailadres, Wachtwoord) VALUES (@naam, @geslacht, @emailadres, @wachtwoord)";
                                cmd.Connection = conn;

                                cmd.Parameters.AddWithValue("@naam", gebruiker.Naam);
                                cmd.Parameters.AddWithValue("@geslacht", gebruiker.Geslacht.ToString());
                                cmd.Parameters.AddWithValue("@emailadres", gebruiker.Emailadres);
                                cmd.Parameters.AddWithValue("@wachtwoord", gebruiker.Wachtwoord);

                                cmd.ExecuteNonQuery();
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
                        // Als naam en gebruikersnaam is ingevoerd
                        else if (gebruiker.Naam != null && gebruiker.Gebruikersnaam != null)
                        {
                            try
                            {
                                cmd.CommandText =
                                    "INSERT INTO Gebruiker (Naam, Gebruikersnaam, Geslacht, Emailadres, Wachtwoord) VALUES (@naam, @gebruikersnaam, @geslacht, @emailadres, @wachtwoord)";
                                cmd.Connection = conn;

                                cmd.Parameters.AddWithValue("@naam", gebruiker.Naam);
                                cmd.Parameters.AddWithValue("@gebruikersnaam", gebruiker.Gebruikersnaam);
                                cmd.Parameters.AddWithValue("@geslacht", gebruiker.Geslacht.ToString());
                                cmd.Parameters.AddWithValue("@emailadres", gebruiker.Emailadres);
                                cmd.Parameters.AddWithValue("@wachtwoord", gebruiker.Wachtwoord);

                                cmd.ExecuteNonQuery();
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
            }
        }

        public void RemoveGebruiker(int gebruikernr)
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
                                "DELETE FROM Gebruiker WHERE Gebruikernr = @gebrnr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@gebrnr", gebruikernr);

                            cmd.ExecuteNonQuery();
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
        }

        public void EditGebruiker(Gebruiker gebruiker)
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
                                "UPDATE Gebruiker SET Naam = @naam, Gebruikersnaam = @gebrnaam, Emailadres = @email, Wachtwoord = @ww WHERE Gebruikernr = @gebrnr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", gebruiker.Naam);
                            cmd.Parameters.AddWithValue("@gebrnaam", gebruiker.Gebruikersnaam);
                            cmd.Parameters.AddWithValue("@email", gebruiker.Emailadres);
                            cmd.Parameters.AddWithValue("@gebrnr", gebruiker.Gebruikernr);
                            cmd.Parameters.AddWithValue("@ww", gebruiker.Wachtwoord);

                            cmd.ExecuteNonQuery();
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
        }

        public Gebruiker LoginGebruiker(string email, string wachtwoord)
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
                            cmd.CommandText = "SELECT * FROM Gebruiker WHERE Emailadres = @email AND Wachtwoord = @wachtwoord";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@wachtwoord", wachtwoord);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
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
                                string Email = reader.GetString(5);
                                string Wachtwoord = reader.GetString(6);
                                return new Gebruiker(gebrnr, abonnement, naam, gebruikersnaam, geslacht, Email,
                                    Wachtwoord);
                            }
                        }
                        catch 
                        {
                            return null;
                        }
                        finally
                        {
                            //Het sluiten van de verbinding met de database.
                            conn.Close();
                        }
                    }
                }
            }
            return null;
        }

        public Gebruiker GebruikerByEmail(string email)
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
                            cmd.CommandText = "SELECT * FROM Gebruiker WHERE Emailadres = @email";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@email", email);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
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
                                string Email = reader.GetString(5);
                                string Wachtwoord = reader.GetString(6);
                                return new Gebruiker(gebrnr, abonnement, naam, gebruikersnaam, geslacht, Email,
                                    Wachtwoord);
                            }
                        }
                        catch
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