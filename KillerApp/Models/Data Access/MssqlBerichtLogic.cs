using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace KillerApp.Models.Data_Access
{
    public class MssqlBerichtLogic : IBerichtServices
    {
        //Connectiestring met database
        private const string Connectie =
            "Server=mssql.fhict.local;Database=dbi347556;User Id=dbi347556;Password=Qwerty1";

        //private const string Connectie =
        //    "Server=MSI;Database=KillerApp;Trusted_Connection=Yes;";

        public List<Bericht> Berichten(Gebruiker gebruiker)
        {
            using (SqlConnection conn = new SqlConnection(Connectie))
            {
                List<Bericht> berichten = new List<Bericht>();

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "SELECT * FROM Bericht WHERE Ontvanger = @ontvanger";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@ontvanger", gebruiker.Gebruikernr);

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int berichtnr = reader.GetInt32(0);
                                Gebruiker verzender = SelectGebruiker(reader.GetInt32(1));
                                Gebruiker ontvanger = SelectGebruiker(reader.GetInt32(2));
                                string titel = reader.GetString(3);
                                string tekst = reader.GetString(4);

                                berichten.Add(new Bericht(berichtnr, verzender, ontvanger, titel, tekst));
                            }
                            return berichten;
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

        public void SendBericht(Bericht bericht)
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
                                "INSERT INTO Bericht (Verzender, Ontvanger, Titel, Tekst) VALUES (@verzender, @ontvanger, @titel, @tekst)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@verzender", bericht.Verzender.Gebruikernr);
                            cmd.Parameters.AddWithValue("@ontvanger", bericht.Ontvanger.Gebruikernr);
                            cmd.Parameters.AddWithValue("@titel", bericht.Titel);
                            cmd.Parameters.AddWithValue("@tekst", bericht.Tekst);

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

        public Gebruiker SelectGebruiker(int gebruikernr)
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
                            return new Gebruiker(gebrnr, abonnement, naam, gebruikersnaam, geslacht, email, wachtwoord);
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

        public void RemoveBericht(int berichtnr)
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
                                "DELETE FROM Bericht WHERE Berichtnr = @berichtnr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@berichtnr", berichtnr);

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
}