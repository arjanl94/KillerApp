using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class MssqlAbonnementLogic : IAbonnementServices
    {
        //Connectiestring met database
        private const string Connectie =
            "Server=mssql.fhict.local;Database=dbi347556;User Id=dbi347556;Password=Qwerty1";

        //private const string Connectie =
        //    "Server=MSI;Database=KillerApp;Trusted_Connection=Yes;";

        public List<Abonnement> ListAbonnementen()
        {
            using (SqlConnection conn = new SqlConnection(Connectie))
            {
                List<Abonnement> abonnementen = new List<Abonnement>();

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "SELECT * FROM Abonnement";
                            cmd.Connection = conn;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                string naam = reader.GetString(0);
                                double prijs = reader.GetDouble(1);
                                string beschrijving = reader.GetString(2);

                                abonnementen.Add(new Abonnement(naam, prijs, beschrijving));
                            }
                            // retourneer de lisjt met abonnementen
                            return abonnementen;
                        }
                        catch (Exception ex)
                        {

                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            // Het sluiten van de verbinding met de database
                            conn.Close();
                        }
                    }
                }
                // Als het niet lukt wordt er niks geretourneert
                return null;
            }
        }

        public void AddAbonnement(Abonnement abonnement)
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
                                "INSERT INTO Abonnement (Naam, Prijs, Beschrijving) VALUES (@naam, @prijs, @beschrijving)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", abonnement.Naam);
                            cmd.Parameters.AddWithValue("@prijs", abonnement.Prijs);
                            cmd.Parameters.AddWithValue("@beschrijving", abonnement.Beschrijving);

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

        public void RemoveAbonnement(string naam)
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
                                "DELETE FROM Abonnement WHERE Naam = @naam";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", naam);

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

        public void EditAbonnement(Abonnement abonnement)
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
                                "UPDATE Abonnement SET Prijs = @prijs, Beschrijving = @beschrijving WHERE Naam = @naam";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@naam", abonnement.Naam);
                            cmd.Parameters.AddWithValue("@prijs", abonnement.Prijs);
                            cmd.Parameters.AddWithValue("@beschrijving", abonnement.Beschrijving);

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