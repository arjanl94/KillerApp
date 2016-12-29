using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class MssqlReactieLogic : IReactieServices
    {
        //Connectiestring met database
        private const string Connectie =
            "Server=mssql.fhict.local;Database=dbi347556;User Id=dbi347556;Password=Qwerty1";
        
        //    "Server=MSI;Database=KillerApp;Trusted_Connection=Yes;";

        public List<Reactie> ListContentReacties(int contentnr)
        {
            using (SqlConnection conn = new SqlConnection(Connectie))
            {
                List<Reactie> reacties = new List<Reactie>();

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "SELECT * FROM Reactie WHERE Contentnr = @contentnr";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@contentnr", contentnr);

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int reactienr = reader.GetInt32(0);
                                Gebruiker schrijver = SelectGebruiker(reader.GetInt32(1));
                                string tekst = reader.GetString(2);

                                reacties.Add(new Reactie(reactienr, schrijver, tekst));
                            }
                            return reacties;
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

        public void AddReactie(Reactie reactie)
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
                            cmd.CommandText = "INSERT INTO Reactie (Reactie_gebr, Tekst, Contentnr) VALUES (@gebrnr, @tekst, @contentnr)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@gebrnr", reactie.Gebruiker.Gebruikernr);
                            cmd.Parameters.AddWithValue("@tekst", reactie.Tekst);
                            cmd.Parameters.AddWithValue("@contentnr", reactie.Contentnr);

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

        //Een reactie wordt geschreven door een gebruiker. Vandaar dat hiervoor een methode is toegevoegd.
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
    }
}