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
                        catch (Exception)
                        {

                            throw;
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
            throw new NotImplementedException();
        }

        public void RemoveAbonnement(Abonnement abonnement)
        {
            throw new NotImplementedException();
        }

        public void EditAbonnement(Abonnement abonnement)
        {
            throw new NotImplementedException();
        }
    }
}